using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            comboBoxmethods.Items.Add("PostBusinessFormInfo");// 
            comboBoxmethods.Items.Add("GetBusinessInfoByOdc");
            comboBoxmethods.Items.Add("dueAndChangeCertification");
            comboBoxmethods.Items.Add("generalChangeCertification");
            comboBoxmethods.Items.Add("RetrieveCorporateInfo");

            comboBoxmethods.Items.Add("SendCorporateInfo");
            comboBoxmethods.Items.Add("RetrieveCellPhoneNumber");
            comboBoxmethods.Items.Add("SendIdentityCardInfo");
            comboBoxmethods.Items.Add("GET_QUEUE_NUM");
          //  comboBoxmethods.Items.Add("GET_VERSION");
            comboBoxmethods.SelectedIndex = 0;
        }

        private void buttonstart_Click(object sender, EventArgs e)
        {
            var threadcount = int.Parse(textBoxthreadcount.Text);

            richTextBox1.AppendText(Environment.NewLine + "test starting..." + threadcount);
            var Volume = int.Parse(textBoxeachthreadvolume.Text);
             var ordinal = int.Parse(textBoxordinal.Text);
            for (int i = 0; i < threadcount; i++)
            {
            //    Thread.Sleep(1000 * 60 * 1);
                _ttt = new Thread(OneThread);
                _ttt.Start(new Threadparam{Method = comboBoxmethods.Text,Volume=int.Parse(textBoxeachthreadvolume.Text),Ordinal = i*Volume+ordinal,
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
                        case "GetBusinessInfoByOdc":
                            param =
                                JsonConvert.SerializeObject(new BusinessModel
                                {
                                    unloadTaskNum =( paramb.Ordinal+i).ToString(),
                                    businessCategory = "HE",
                                    userName = paramb.UserName,
                                    countyCode = paramb.CountyCode,
                                    queueNum = "50010",
                                    type = paramb.Type,
                                    address = "wolong"
                                });
                            var ret = aa.SendRestHttpClientRequest(homeurl, method, param);
                            //  richTextBox1.AppendText(Environment.NewLine + "输入：" + param + "     输出:" + aa.SendRestHttpClientRequest(homeurl, method, param));
                            watch.Stop();
                            BeginInvoke(new UpdateStatusDelegate(UpdateStatus), new object[]
                            {
                                string.Format("thread {1},{0} transaction, result = {2}, elapsed time {3} milliseconds,{4}",
                                    method, paramb.Ordinal, 
                                   // ret, //url={4}
                                    JsonConvert.DeserializeObject<ResultModel>(ret).statusCode,
                                    watch.ElapsedMilliseconds,ret.Length)
                            });
                            //BeginInvoke(new UpdateStatusDelegate(UpdateStatus), new object[]
                            //{
                            //    string.Format("{0} transaction, elapsed time {3} milliseconds, input:{1},output:{2}",
                            //        method, param, ret, watch.ElapsedMilliseconds)
                            //});
                            break;
                        case "PostBusinessFormInfo":
                            param =
                                JsonConvert.SerializeObject(new BusinessModel
                                {
                                    businessCategory = "HE",
                                    userName = paramb.UserName,
                                    countyCode = paramb.CountyCode,
                                    unloadTaskNum =( paramb.Ordinal + i).ToString(),
                                    queueNum = "50010",
                                    type = paramb.Type,
                                    checkFile = paramb.Ordinal+i,
                                    zipFile = File.ReadAllBytes(@textBoxsourcefile.Text),
                                    address = "wolong"
                                });
                            var bb = aa.SendRestHttpClientRequest(homeurl, method, param);
                            //  richTextBox1.AppendText(Environment.NewLine + "输入：" + param + "     输出:" + aa.SendRestHttpClientRequest(homeurl, method, param));
                            watch.Stop();
                            var theret = JsonConvert.DeserializeObject<ResultModel>(bb);
                            BeginInvoke(new UpdateStatusDelegate(UpdateStatus), new object[]
                            {
                                string.Format("thread {1},{0} transaction, result = {2}, elapsed time {3} milliseconds,{4}",
                                    method, paramb.Ordinal, 
                                   // ret, //url={4}
                                    theret.statusCode,
                                    watch.ElapsedMilliseconds,theret.result)
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
                                new object[] { string.Format("no this method, " + method) });
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

        private void buttonstuff_Click(object sender, EventArgs e)
        {
            try
            {
                var threadcount = int.Parse(textBoxthreadcount.Text);
                var ordinal = int.Parse(textBoxordinal.Text);
                var Volume = int.Parse(textBoxeachthreadvolume.Text);
                richTextBox1.AppendText(Environment.NewLine + "test starting..." + threadcount);
                for (int i = 0; i < threadcount; i++)
                {
                    _ttt = new Thread(StuffThread);
                    _ttt.Start(new Threadparam
                    {
                        Method = comboBoxmethods.Text,
                        Volume = Volume,
                        Ordinal = i*Volume+ordinal,
                        UserName = textBoxusername.Text,
                        Type = int.Parse(textBoxtype.Text),
                        CountyCode = textBoxcountcode.Text,
                        Server = textBoxurl.Text
                    });
                }
              
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText(Environment.NewLine+ex);
            }
        }

        private void StuffThread(object obj)
        {
            try
            {
                var input = (Threadparam)obj;
                var stop = new Stopwatch();
                stop.Start();
              //  var volume = int.Parse(textBoxdatavolume.Text);
                using (var db = new Model1())
                {
                    for (int i = 0; i < input.Volume; i++)
                    {
                        db.fushanbusiness.Add(new fushanbusiness
                        {
                            COUNTYCODE = "fushan",
                            ID = i+input.Ordinal,
                            TYPE = i + input.Ordinal,
                            START_TIME = "2011-1-1",
                            PROCESS_USER = input.UserName,
                            UNLOAD_TASK_NUM = (i + input.Ordinal).ToString(),
                            STATUS = 1,
                            QUEUE_NUM = (i + input.Ordinal).ToString(),

                        });
                        db.SaveChangesAsync();
                    }
                }
                stop.Stop();
                BeginInvoke(new UpdateStatusDelegate(UpdateStatus),
                              new object[] { string.Format("{1} data inserted, elapsed time:{0}",stop.ElapsedMilliseconds,input.Volume) });
            }
            catch (Exception ex)
            {
                BeginInvoke(new UpdateStatusDelegate(UpdateStatus),
                              new object[] { string.Format("StuffThread, some error occurred,{0}", ex.Message) });
            }
        }

        private void buttonstop_Click(object sender, EventArgs e)
        {
            try
            {
                var threadcount = int.Parse(textBoxthreadcount.Text);
                var ordinal = int.Parse(textBoxordinal.Text);
                var Volume = int.Parse(textBoxeachthreadvolume.Text);
                richTextBox1.AppendText(Environment.NewLine + "test starting..." + threadcount);
                for (int i = 0; i < threadcount; i++)
                {
                    _ttt = new Thread(StopThread);
                    _ttt.Start(new Threadparam
                    {
                        Method = comboBoxmethods.Text,
                        Volume = Volume,
                        Ordinal = i * Volume + ordinal,
                        UserName = textBoxusername.Text,
                        Type = int.Parse(textBoxtype.Text),
                        CountyCode = textBoxcountcode.Text,
                        Server = textBoxurl.Text
                    });
                }

            }
            catch (Exception ex)
            {
                richTextBox1.AppendText(Environment.NewLine + ex);
            }
        }

        private void StopThread(object obj)
        {
            try
            {
                var input = (Threadparam)obj;
                var stop = new Stopwatch();
                stop.Start();
                //  var volume = int.Parse(textBoxdatavolume.Text);
                using (var db = new Model1())
                {
                    var datasets =
                        db.fushanbusiness.Where(a => a.ID >= input.Ordinal && a.ID < input.Ordinal + input.Volume);
                    foreach (fushanbusiness dataset in datasets)
                    {
                        dataset.UNLOAD_TASK_NUM = dataset.ID.ToString();

                    }
                  
                        db.SaveChangesAsync();

                }
                stop.Stop();
                BeginInvoke(new UpdateStatusDelegate(UpdateStatus),
                              new object[] { string.Format("{1} data update, elapsed time:{0}", stop.ElapsedMilliseconds, input.Volume) });
            }
            catch (Exception ex)
            {
                BeginInvoke(new UpdateStatusDelegate(UpdateStatus),
                              new object[] { string.Format("StuffThread, some error occurred,{0}", ex.Message) });
            }
        }

        private void buttonfiles_Click(object sender, EventArgs e)
        {
            var currentdate = DateTime.Now.Date;
            var scurrentdate = string.Format("{0}-{1}-{2}", currentdate.Year, currentdate.Month, currentdate.Day);

            var filepath = string.Format("{2}{0}\\{1}", textBoxcountcode.Text, scurrentdate, @"d:\\");
            //    Log.Info("path 11 =" + filepath);
            if (!Directory.Exists(@filepath))
            {
                richTextBox1.AppendText(Environment.NewLine + "path=" + filepath);
                Directory.CreateDirectory(@filepath);
            }
            var start = int.Parse(textBoxordinal.Text);
            var volume = int.Parse(textBoxdatavolume.Text);
            var zipFile = File.ReadAllBytes(@textBoxsourcefile.Text);
            for (int i = start; i < volume; i++)
            {
                var filename = string.Format("{0}\\{1}", filepath, i);
                richTextBox1.AppendText(Environment.NewLine + "file name=" + filename);
                File.WriteAllBytes(filename, zipFile);
            }
        }

        
        private void textBoxsourcefile_TextChanged(object sender, EventArgs e)
        { 
            richTextBox1.AppendText(Environment.NewLine + string.Format("{0},{1},{2},{3}", (int)'a', (int)'z', (int)'A',""));
        }
    }
}
