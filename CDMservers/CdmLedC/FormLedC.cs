using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using Message = System.Messaging.Message;

namespace CdmLedC
{
    public partial class FormLedC : Form
    {
        private delegate void UpdateStatusDelegate(string status);
        private IHubProxy HubProxy { set; get; }
        private HubConnection Connection { get; set; }
        private bool IsSignalrConnected = false;
        private Thread _tCheckSignalr;
        private const string QueueName = ".\\private$\\" + "CdmLedC";

        public FormLedC()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxserver.Text = ConfigurationManager.AppSettings["cdmServer"];
            textBoxreject.Text = ConfigurationManager.AppSettings["reject"];
            textBoxdone.Text = ConfigurationManager.AppSettings["done"];
            textBoxprocessing.Text = ConfigurationManager.AppSettings["processing"];
            textBoxcounty.Text = ConfigurationManager.AppSettings["countyCode"];
            CreateNewQueue();
        }
        private void CreateNewQueue()
        {
            try
            {

                if (MessageQueue.Exists(QueueName))
                {
                    richTextBoxLog.AppendText(Environment.NewLine + QueueName + "已经存在");
                }
                else
                {
                    var mq = MessageQueue.Create(QueueName);
                    mq.Label = QueueName;
                    richTextBoxLog.AppendText(Environment.NewLine + QueueName + "创建成功");
                    mq.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);
                    mq.SetPermissions("ANONYMOUS LOGON", MessageQueueAccessRights.FullControl);
                    mq.SetPermissions("ENTERPRISE DOMAIN CONTROLLERS", MessageQueueAccessRights.FullControl);
                    mq.SetPermissions("SELF", MessageQueueAccessRights.FullControl);

                }
            }
            catch (Exception ex)
            {
                richTextBoxLog.AppendText(Environment.NewLine+ex.Message);
            }
        }
        private object LedMessageProcessing(CdmMessage mcc)
        {
            richTextBoxLog.AppendText(Environment.NewLine + JsonConvert.SerializeObject(mcc));
            try
            {
                var voice = string.Empty;
                switch (mcc.LedMsgType)
                {
                    case LedMsgType.Processing:
                        voice = textBoxprocessing.Text.Replace("queueNum", mcc.Content);
                        break;
                    case LedMsgType.Done:
                        voice = textBoxdone.Text.Replace("queueNum", mcc.Content);
                        break;
                    default:
                        voice = textBoxreject.Text.Replace("queueNum", mcc.Content);
                        break;
                }

                MsmqMsgSend(voice);
                richTextBoxLog.AppendText(Environment.NewLine + voice);
            }
            catch (Exception ex)
            {
                richTextBoxLog.AppendText(Environment.NewLine + ex.Message);
            }
            return string.Empty;
        }

        private void MsmqMsgSend(string voice)
        {
            using (var mq = new MessageQueue(QueueName))
            {
                var msg = new Message
                {
                    Label = "[cdmLedC]" ,
                    Recoverable = true,
                    Body = voice
                };
                mq.Send(msg);
                richTextBoxLog.AppendText(Environment.NewLine + string.Format("【成功发送消息】{0}，{1}", msg.Body, DateTime.Now));
            }
        }

        private void CheckSignalr()
        {
            do
            {
                var server = string.Format("http://{0}/", textBoxserver.Text);
                Connection = new HubConnection(server);
                HubProxy = Connection.CreateHubProxy("Cdmhub");
                BeginInvoke(new UpdateStatusDelegate(UpdateStatus), new object[] { string.Format("signalr 查询: {0}", "CreateHubProxy ok") });
                HubProxy.On<CdmMessage>("LedMessage", (mcc) =>
                    this.Invoke((Action)(() => LedMessageProcessing(mcc)
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
        private async void connectSignalr()
        {
            try
            {
                try
                {
                    await Connection.Start();
                    BeginInvoke(new UpdateStatusDelegate(UpdateStatus), new object[] { string.Format("signalr 查询: {0}", "start ok") });
                    await HubProxy.Invoke("Login", new CdmClient { CountyCode = textBoxcounty.Text, ClientType = ClientType.Led });
                    BeginInvoke(new UpdateStatusDelegate(UpdateStatus), new object[] { "signalr login ok" });
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
        private void UpdateStatus(string status)
        {
            richTextBoxLog.AppendText(Environment.NewLine + string.Format("{0}--{1}", DateTime.Now, status));
            richTextBoxLog.SelectionStart = richTextBoxLog.Text.Length;
            richTextBoxLog.ScrollToCaret();
        }
        private void buttonlogin_Click(object sender, EventArgs e)
        {
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
        private void textBoxprocessing_TextChanged(object sender, EventArgs e)
        {
            Setconfig("processing", textBoxprocessing.Text);
        }

        private void textBoxreject_TextChanged(object sender, EventArgs e)
        {
            Setconfig("reject", textBoxreject.Text);
        }

        private void textBoxdone_TextChanged(object sender, EventArgs e)
        {
            Setconfig("done", textBoxdone.Text);
        }

        private void textBoxcounty_TextChanged(object sender, EventArgs e)
        {
            Setconfig("countyCode", textBoxcounty.Text);
        }

        private void textBoxserver_TextChanged(object sender, EventArgs e)
        {
            Setconfig("cdmServer", textBoxserver.Text);
        }

        private async void buttonrejecttest_Click(object sender, EventArgs e)
        {
            await HubProxy.Invoke("LedMessage", new CdmMessage { CountyCode = textBoxcounty.Text, ClientType = ClientType.Led, Content = "30005", LedMsgType = LedMsgType.Reject });
        }

        private async void buttondonetest_Click(object sender, EventArgs e)
        {
            await HubProxy.Invoke("LedMessage", new CdmMessage { CountyCode = textBoxcounty.Text, ClientType = ClientType.Led, Content = "30006", LedMsgType = LedMsgType.Done });
        }
    }
}
