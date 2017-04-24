using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System.Speech.Synthesis;
  

namespace CdmCnf45
{
    public partial class FormVoice : Form
    {
        private delegate void UpdateStatusDelegate(string status);
        private IHubProxy HubProxy { set; get; }
        private HubConnection Connection { get; set; }
        private bool IsSignalrConnected = false;
        private Thread _tCheckSignalr;
        public FormVoice()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxserver.Text = ConfigurationManager.AppSettings["cdmServer"];
            textBoxreject.Text = ConfigurationManager.AppSettings["rejectVoice"];
            textBoxfeevoice.Text = ConfigurationManager.AppSettings["feeVoice"];
            textBoxcounty.Text = ConfigurationManager.AppSettings["countyCode"];
        }
        private void CheckSignalr()
        {
            do
            {
                var server = string.Format("http://{0}/", textBoxserver.Text);
                Connection = new HubConnection(server);
                HubProxy = Connection.CreateHubProxy("Cdmhub");
                BeginInvoke(new UpdateStatusDelegate(UpdateStatus), new object[] { string.Format("signalr 查询: {0}", "CreateHubProxy ok")});
                HubProxy.On<CdmMessage>("VoiceMessage", (mcc) =>
                    this.Invoke((Action)(() => VoiceMessageProcessing(mcc)
                    ))
                );

                BeginInvoke(new UpdateStatusDelegate(UpdateStatus), new object[] { string.Format("signalr 查询: {0}", "HubProxy.On ok") });
                connectSignalr();
                Thread.Sleep(1000 * 60 * 1);
            } while (!IsSignalrConnected);
            do
            {
                try
                {
                    if (Connection.State.Equals(Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected))
                    {
                        BeginInvoke(new UpdateStatusDelegate(UpdateStatus), new object[] { string.Format("CheckSignalr disconnected, reconnecting:{0}", textBoxserver.Text) });
                        connectSignalr();
                    }
                    else
                    {

                      //  BeginInvoke(new UpdateStatusDelegate(UpdateStatus), new object[] { string.Format("CheckSignalr detecting:{0}, connected", textBoxserver.Text) });
                    }
                }
                catch (Exception ex)
                {
                    BeginInvoke(new UpdateStatusDelegate(UpdateStatus), new object[] { string.Format("CheckSignalr reconnecting error:{0},{1}", 
                        textBoxserver.Text,ex.Message) });
                }
                Thread.Sleep(1000 * 60 * 1);
            } while (true);
            // ReSharper disable once FunctionNeverReturns
        }
        private void UpdateStatus(string status)
        {
            richTextBox1.AppendText(Environment.NewLine + string.Format("{0}--{1}", DateTime.Now, status));
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
        private async void connectSignalr()
        {
            try
            {
                try
                {
                    await Connection.Start();
                    BeginInvoke(new UpdateStatusDelegate(UpdateStatus), new object[] {string.Format("signalr 查询: {0}","start ok")});
                    await HubProxy.Invoke("Login", new CdmClient { CountyCode = textBoxcounty.Text, ClientType = ClientType.Voice });
                    BeginInvoke(new UpdateStatusDelegate(UpdateStatus), new object[] { "signalr login ok"});
                    IsSignalrConnected = true;
                }
                catch (Exception hex)
                {
                    //if (hex.Message.Contains("发送请求时出错"))
                    //    richTextBox1.AppendText(string.Format("signalr 查询:url={0},{1}", server, "网站可能在更新，稍后再查。"));
                    //else
                    BeginInvoke(new UpdateStatusDelegate(UpdateStatus), new object[] { string.Format("signalr 查询: error.{0}", hex.Message) });
                    IsSignalrConnected = false;
                }
            }
            catch (Exception ex)
            {
                BeginInvoke(new UpdateStatusDelegate(UpdateStatus), new object[] { string.Format("CheckSignalr error:{0}", ex.Message) });
            }
        }

        private object VoiceMessageProcessing(CdmMessage mcc)
        {
            richTextBox1.AppendText(Environment.NewLine + JsonConvert.SerializeObject(mcc));
            try
            {
                Type type = Type.GetTypeFromProgID("SAPI.SpVoice");

                dynamic spVoice = Activator.CreateInstance(type);
                var voice = string.Empty;
                switch (mcc.VoiceType)
                {
                    case VoiceType.Fee:
                        voice = textBoxfeevoice.Text.Replace("queueNum", mcc.Content);
                        break;
                    default:
                        voice = textBoxreject.Text.Replace("queueNum", mcc.Content);
                        break;
                }

                spVoice.Speak(voice);
                richTextBox1.AppendText(Environment.NewLine + voice);
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText(Environment.NewLine + ex.Message);
            }
            return string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var a = new Dictionary<string, bool> { { "yw28", true }, { "aa", false } };
                var b = JsonConvert.SerializeObject(a);
                richTextBox1.AppendText(Environment.NewLine+b);
                var c = new UserTransaction();
                c.UserInfo=new PoliceUser();
                c.UserInfo.Permission = a;
                var d = JsonConvert.SerializeObject(c);
                richTextBox1.AppendText(Environment.NewLine + d);
                Type type = Type.GetTypeFromProgID("SAPI.SpVoice");

                dynamic spVoice = Activator.CreateInstance(type);

                spVoice.Speak("你好,欢迎使用！");
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText(Environment.NewLine+ex.Message);
            }
            _tCheckSignalr = new Thread(new ThreadStart(CheckSignalr));
            _tCheckSignalr.Start();
        }

        private void Setconfig(string p1, string p2)
        {
            if (p1 == null || p2 == null) return;
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var appSection = (AppSettingsSection)config.GetSection("appSettings");

            appSection.Settings[p1].Value = p2;
            config.Save();
        }

        private void textBoxserver_TextChanged(object sender, EventArgs e)
        {
            Setconfig("cdmServer", textBoxserver.Text);
        }

        private void textBoxfeevoice_TextChanged(object sender, EventArgs e)
        {
            Setconfig("feeVoice", textBoxfeevoice.Text);
        }

        private void textBoxreject_TextChanged(object sender, EventArgs e)
        {
            Setconfig("rejectVoice", textBoxreject.Text);
        }

        private void FormVoice_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
         //   try { _tCheckSignalr.Abort(); }
         //catch(Exception){}
        }

        private async void buttonfeetest_Click(object sender, EventArgs e)
        {
            await HubProxy.Invoke("VoiceMessage", new CdmMessage { CountyCode = textBoxcounty.Text,ClientType = ClientType.Voice, Content = "20003",VoiceType= VoiceType.Fee });
        }

        private async void buttonrejecttest_Click(object sender, EventArgs e)
        {
            await HubProxy.Invoke("VoiceMessage", new CdmMessage { CountyCode = textBoxcounty.Text, ClientType = ClientType.Voice, Content = "30005", VoiceType = VoiceType.Reject });
        }

        private void textBoxcounty_TextChanged(object sender, EventArgs e)
        {
            Setconfig("countyCode", textBoxcounty.Text);
        }
    }
}
