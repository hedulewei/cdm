﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Common;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;

namespace VoicePlay
{
    class Program : IDisposable
    {
        private static int voicecount = 2;
        private static int voiceinterval = 2000;
        private static IHubProxy HubProxy { set; get; }
        private static HubConnection Connection { get; set; }
      //  private Thread _tCheckSignalr;
        private static Mutex _lockvoiceMutex = new Mutex();
        private static string traceFile;
        public void Dispose()
        {
            if (Connection != null)
            {
                Connection.Dispose();
                Connection = null;
            }
            if (_lockvoiceMutex != null)
            {
                _lockvoiceMutex.Dispose();
                _lockvoiceMutex = null;
            }
        }
        static void Main(string[] args)
        {
            //var traceFile = GetTraceFile();
            traceFile = GetTraceFile();
            //Trace.AutoFlush = true;
            //Trace.Listeners.Clear();
            //Trace.Listeners.Add(new TextWriterTraceListener(traceFile));

            var vc = ConfigurationManager.AppSettings["voiceCount"];
            var vi = ConfigurationManager.AppSettings["voiceInterval"];
            int count;
            if (int.TryParse(vc, out count)) voicecount = count;
            if (int.TryParse(vi, out count)) voiceinterval = count;
            CheckSignalr();
            //_tCheckSignalr = new Thread(new ThreadStart(CheckSignalr));
            //_tCheckSignalr.Start();
        }
        private static string GetTraceFile()
        {
            var basePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var date = DateTime.Now.Date.ToString("yy-MM-dd");
            var traceFile = basePath + "\\CdmVoiceLog" + date + ".txt";
            return traceFile;
        }

        private static void CheckSignalr()
        {
            var server = string.Format("http://{0}/", GetAppConfig("cdmServer"));
            Connection = new HubConnection(server);
            HubProxy = Connection.CreateHubProxy("Cdmhub");

            TraceLog(string.Format("signalr 查询: {0}", "CreateHubProxy ok"));

            HubProxy.On<CdmMessage>("VoiceMessage", (mcc) => VoiceMessageProcessing(mcc));

            TraceLog(string.Format("signalr 查询: {0}", "HubProxy.On ok"));
            ConnectSignalr();
            do
            {
                try
                {
                    if (Connection.State.Equals(Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected))
                    {
                        TraceLog(string.Format("CheckSignalr disconnected, reconnecting:{0}", GetAppConfig("cdmServer")));
                        ConnectSignalr();
                    }
                }
                catch (Exception ex)
                {
                    TraceLog(string.Format("CheckSignalr reconnecting error:{0},{1}",
                       GetAppConfig("cdmServer"), ex.Message));
                }
                Thread.Sleep(1000 * 60);
            } while (true);
            // ReSharper disable once FunctionNeverReturns
        }
        private static object VoiceMessageProcessing(CdmMessage mcc)
        {
            TraceLog(string.Format("voice message received:{0}", JsonConvert.SerializeObject(mcc)));
            try
            {
                //  TraceLog(string.Format("VoiceMessageProcessing error:{0}", 111));
                var t = new Thread(new ParameterizedThreadStart(VoiceBroadcast));
                //   TraceLog(string.Format("VoiceMessageProcessing error:{0}", 222));
                t.Start(mcc);
                //  TraceLog(string.Format("VoiceMessageProcessing error:{0}", 333));
            }
            catch (Exception ex)
            {
                TraceLog(string.Format("VoiceMessageProcessing error:{0}", ex));
            }

            return string.Empty;
        }
        private static async void ConnectSignalr()
        {
            try
            {
                try
                {
                    await Connection.Start();
                    TraceLog(string.Format("signalr 查询: {0}", "start ok"));
                    await HubProxy.Invoke("Login", new CdmClient { CountyCode = GetAppConfig("countyCode"), ClientType = ClientType.Voice });
                    TraceLog("signalr login ok");
                    //  IsSignalrConnected = true;
                }
                catch (Exception hex)
                {
                    TraceLog(string.Format("signalr 查询: error.{0}", hex.Message));
                    //  IsSignalrConnected = false;
                }
            }
            catch (Exception ex)
            {
                TraceLog(string.Format("CheckSignalr error:{0}", ex.Message));
            }
        }
        private static void VoiceBroadcast(object p)
        {
            try
            {
                //  TraceLog(string.Format("VoiceBroadcast error:{0}", 11));
                lock (_lockvoiceMutex)
                {
                    //  TraceLog(string.Format("VoiceBroadcast error:{0}", 22));
                    var mcc = (CdmMessage)p;
                    TraceLog(string.Format("VoiceBroadcast error:{0}", 33));
                    Type type = Type.GetTypeFromProgID("SAPI.SpVoice");
                    TraceLog(string.Format("VoiceBroadcast error:{0}", 3333333));
                    dynamic spVoice = Activator.CreateInstance(type);

                    //   var spVoice1 = new SpVoice();
                    //    TraceLog(string.Format("VoiceBroadcast error:{0}", 55));
                    string voice;
                    switch (mcc.VoiceType)
                    {
                        case VoiceType.Fee:
                            voice = GetAppConfig("feeVoice").Replace("queueNum", mcc.Content);
                            break;
                        case VoiceType.PlayOver:
                            voice = mcc.Content;
                            break;
                        default:
                            voice = GetAppConfig("rejectVoice").Replace("queueNum", mcc.Content);
                            break;
                    }
                    // TraceLog("first voice begin");
                    for (int i = 0; i < voicecount; i++)
                    {
                        TraceLog(string.Format("{0},第{1}次呼叫开始,{2}", 123, i + 1, voice));
                        spVoice.Speak(voice);
                        Thread.Sleep(voiceinterval);
                        TraceLog(string.Format("{0},第{1}次呼叫结束,{2}", 123, i + 1, voice));
                    }
                }
            }
            catch (Exception ex)
            {
                TraceLog(ex.Message);
            }
        }
        private static void TraceLog(string format)
        {
           // Trace.TraceInformation("{0}--{1}", DateTime.Now, format);
         //   Console.WriteLine("{0}--{1}", DateTime.Now, format);
            WritelogStatic(format);
        }
        static void WritelogStatic(string logtext)
        {
            try
            {
                using (var sw = System.IO.File.AppendText(traceFile))
                {
                    sw.WriteLine(DateTime.Now.ToLocalTime() + "---" + logtext);
                    sw.Close();
                }
            }
            catch (Exception) { }
        }
        private static string GetAppConfig(string key)
        {
            return ConfigurationManager.AppSettings[key]; ;
        }
    }
}
