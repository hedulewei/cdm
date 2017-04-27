using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using Newtonsoft.Json;

namespace Stress_and_Performance_Testing
{
    public partial class Formtest : Form
    {
        private readonly List<Thread> _threads=new List<Thread>();
        private delegate void UpdateStatusDelegate(string status);

        private Thread _ttt;
        public Formtest()
        {
            InitializeComponent();
            comboBoxmethods.Items.Add("PostBusinessFormInfo");
            comboBoxmethods.SelectedIndex = 0;
        }

        private void buttonstart_Click(object sender, EventArgs e)
        {
            var threadcount = int.Parse(textBoxthreadcount.Text);

            richTextBox1.AppendText(Environment.NewLine + "test starting..." + threadcount);
            for (int i = 0; i < threadcount; i++)
            {
            //    Thread.Sleep(1000 * 60 * 1);
                _ttt = new Thread(OneThread);
                _ttt.Start(new Threadparam{Method = comboBoxmethods.Text,Volume=int.Parse(textBoxeachthreadvolume.Text),Ordinal = i,
                    UserName=textBoxusername.Text,
                    Type=int.Parse(textBoxtype.Text),
                    CountyCode=textBoxcountcode.Text,
                    Server=textBoxurl.Text});
            //    _threads.Add(_ttt);
             //   richTextBox1.AppendText(Environment.NewLine + string.Format("thread {0} started...{1}", i, _ttt.IsAlive));
            }
           
        }
        private void UpdateStatus(string status)
        {
            richTextBox1.AppendText(Environment.NewLine + string.Format("{0}--{1}", DateTime.Now, status));
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
        public class Threadparam
        {
            public string Method { get; set; }
            public string CountyCode { get; set; }
            public string UserName { get; set; }
            public string Server { get; set; }
            public int Volume { get; set; }
            public int Type { get; set; }
            public int Ordinal { get; set; }
        }
        private void OneThread(object parama)
        {
            try
            {
                var paramb = (Threadparam) parama;
                //BeginInvoke(new UpdateStatusDelegate(UpdateStatus),
                //                new object[] { string.Format("OneThread starting") });
                var homeurl = paramb.Server;
                string param;
                var method = paramb.Method;
                var volume = paramb.Volume;

                var aa = new CdmCliComNs.CDMCCom();
                for (int i = 0; i < volume; i++)
                {
                    //BeginInvoke(new UpdateStatusDelegate(UpdateStatus),
                    //              new object[] { string.Format("business {0}", i) });
                    var watch = new Stopwatch();
                    watch.Start();
                    switch (method)
                    {
                        case "PostBusinessFormInfo":
                            param =
                                JsonConvert.SerializeObject(new BusinessModel
                                {
                                    businessCategory = "HE",
                                    userName=paramb.UserName,
                                    countyCode=paramb.CountyCode,
                                    queueNum="50010",
                                    type=paramb.Type,
                                    address = "wolong"
                                });
                            var ret = aa.SendRestHttpClientRequest(homeurl, method, param);
                            //  richTextBox1.AppendText(Environment.NewLine + "输入：" + param + "     输出:" + aa.SendRestHttpClientRequest(homeurl, method, param));
                            watch.Stop();
                            BeginInvoke(new UpdateStatusDelegate(UpdateStatus), new object[]
                            {
                                string.Format("thread {1},{0} transaction, result = {2}, elapsed time {3} milliseconds,",
                                    method, paramb.Ordinal, 
                                   // ret, //url={4}
                                    JsonConvert.DeserializeObject<ResultModel>(ret).statusCode,
                                    watch.ElapsedMilliseconds,homeurl)
                            });
                            //BeginInvoke(new UpdateStatusDelegate(UpdateStatus), new object[]
                            //{
                            //    string.Format("{0} transaction, elapsed time {3} milliseconds, input:{1},output:{2}",
                            //        method, param, ret, watch.ElapsedMilliseconds)
                            //});
                            break;
                        case "getordinal":
                            var input = new BusinessModel
                            {
                                countyCode = "haiyang",
                                businessCategory = "yew1",
                                userName = "user1",
                                password = "pass"
                            };
                            var json = JsonConvert.SerializeObject(input);
                            Console.WriteLine("input=" + json);
                            Console.WriteLine("output = " + aa.RestHttpClientGet(textBoxurl.Text, "getordinal", json));
                            break;
                        case "getordinal2":
                            Console.WriteLine(aa.RestHttpClientGet(textBoxurl.Text, "getordinal2",
                                "?code=334&category=0334"));
                            break;
                        case "GET_VERSION":
                            var haha =
                                JsonConvert.SerializeObject(new BusinessModel
                                {
                                    businessCategory = "HE",
                                    address = "wolong"
                                });
                            //    var dict = new Dictionary<string, string>
                            //{
                            //    {"UserName","param1. userName"},
                            //    {"Password"," param1.password"},
                            //    {"grant_type", "password"}
                            //};
                            Console.WriteLine("输入：" + haha + "     输出" +
                                              aa.SendRestHttpClientRequest(homeurl, "GET_VERSION", haha));
                            break;
                        default:
                            BeginInvoke(new UpdateStatusDelegate(UpdateStatus),
                                new object[] { string.Format("no this method" + comboBoxmethods.Text) });
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                BeginInvoke(new UpdateStatusDelegate(UpdateStatus),
                               new object[] { string.Format("some error occurred,{0}" ,ex.Message) });
            }
        }

        private void Formtest_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
