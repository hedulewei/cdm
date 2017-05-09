using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
using Newtonsoft.Json;

namespace CdmWfdc
{
    public partial class Form1 : Form
    {
        private IHubProxy HubProxy { set; get; }
        private HubConnection Connection { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private async void connectSignalr()
        {
            var server = string.Format("http://{0}/", "192.168.0.167");
            try
            {
                Connection=new HubConnection(server);
                HubProxy = Connection.CreateHubProxy("Cdmhub");
                HubProxy.On<CdmMessage>("Message", (mcc) =>
                    this.Invoke((Action)(() => NewMessageProcessing(mcc)
                    ))
                );
           //     HubProxy.On<UpdateInfo>("NoticeUpdate", (ui) =>
           //        this.Invoke((Action)(() => NoticeUpdate(ui)
           //        ))
           //    );
         
                try
                {
                   await Connection.Start();
                     HubProxy.Invoke("Message", new CdmMessage{Title="haha",Content="hehe"});
                }
                catch (Exception hex)
                {
                    if (hex.Message.Contains("发送请求时出错"))
                       richTextBox1.AppendText(string.Format("signalr 查询:url={0},{1}", server, "网站可能在更新，稍后再查。")); 
                    else
                        richTextBox1.AppendText( string.Format("signalr 查询: error.{0}", hex.Message) );
                }
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText( string.Format("CheckSignalr error:{0}", ex.Message) );
            }
        }

        private object NewMessageProcessing(CdmMessage mcc)
        {
            richTextBox1.AppendText(Environment.NewLine+JsonConvert.SerializeObject(mcc));
            return string.Empty;
        }
    }
}
