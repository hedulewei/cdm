using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using FaceDetection;
using AForge.Video;

using AForge.Video.DirectShow;
using Stream = System.IO.Stream;

namespace face
{
    public partial class Form1 : Form
    {//  private VideoSourcePlayer videoSourcePlayer = new AForge.Controls.VideoSourcePlayer();
        private FilterInfoCollection videoDevices;
        private string sourceImage = string.Empty;
        private string currentImage = string.Empty;
        FisherFaceRecognizer recognizer = new FisherFaceRecognizer(10, 10.0);
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();//Images
             List<string> Names_List = new List<string>(); //labels
        List<int> Names_List_ID = new List<int>();
       // List<IInputArray> listmMats = new List<IInputArray>();
        public Form1()
        {
            InitializeComponent();
        }

        private void buttoncompare_Click(object sender, EventArgs e)
        {
            try
            {
              //  currentImage = Run(textBoxcurrentimage.Text);
              //  pictureBoxcurrentimage.Image = Image.FromFile(currentImage);
              //  UpdateStatus(string.Format("exception :{0}",111));

              ////  recognizer.Train();
              ////  ff.Load(currentImage);
              //  UpdateStatus(string.Format("exception :{0}", 333));
                var res = recognizer.Predict(new Image<Gray,Byte>(textBoxcurrentimage.Text));
              UpdateStatus(string.Format("exception :{0}", 444));
              richTextBox1.AppendText(string.Format("{0},Distance={1},Label={2},{3}", Environment.NewLine, res.Distance, res.Label,res));
            }
            catch (Exception ex)
            {
                UpdateStatus(string.Format("exception :{0}",ex.Message));
            }
        }
        private void UpdateStatus(string status)
        {
            richTextBox1.AppendText(Environment.NewLine + string.Format("{0}--{1}", DateTime.Now, status));
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
         string Run(string fname)
        {
            IImage image;

            //Read the files as an 8-bit Bgr image  

            image = new UMat(fname, ImreadModes.Color); //UMat version
            //image = new Mat("lena.jpg", ImreadModes.Color); //CPU version
          //   listmMats.Add(image);
          //  
             var aaa = new Image<Emgu.CV.Structure.Gray , byte>(fname);
              trainingImages.Add(aaa);
            
             Names_List_ID.Add(Names_List_ID.Count());

            long detectionTime;
            List<Rectangle> faces = new List<Rectangle>();
            List<Rectangle> eyes = new List<Rectangle>();
         //   richTextBox1.AppendText(Environment.NewLine + "aaa");
            DetectFace.Detect(
              image, "haarcascade_frontalface_default.xml", "haarcascade_eye.xml",
              faces, eyes,
              out detectionTime);

            foreach (Rectangle face in faces)
                CvInvoke.Rectangle(image, face, new Bgr(Color.Red).MCvScalar, 2);
            foreach (Rectangle eye in eyes)
                CvInvoke.Rectangle(image, eye, new Bgr(Color.Blue).MCvScalar, 2);
         //   richTextBox1.AppendText(Environment.NewLine + "bbb");
            //display the image 
             using (InputArray iaImage = image.GetInputArray())
             {
                 // recognizer.Train(image, );
                 var tempfile = Path.GetTempFileName() + ".jpg";
               //  richTextBox1.AppendText(Environment.NewLine + "ccc");
                 image.Save(tempfile);
                // aaa.Save(tempfile);
                // recognizer.Train(image, Names_List_ID.ToArray());
                // listmMats.Add(iaImage);
                 //var a = new ImageViewer();
                 //a.BackgroundImage = ;
                 //a.ShowDialog();
                 //ImageViewer.Show(image, String.Format(
                 //  //    ImageViewer.Show(image, String.Format(
                 //    "Completed face and eye detection using {0} in {1} milliseconds",
                 //    (iaImage.Kind == InputArray.Type.CudaGpuMat && CudaInvoke.HasCuda) ? "CUDA" :
                 //    (iaImage.IsUMat && CvInvoke.UseOpenCL) ? "OpenCL"
                 //    : "CPU",
                 //    detectionTime));
                 richTextBox1.AppendText(Environment.NewLine + "eee");
                 return tempfile;
             }
        }

         private void buttoncamera_Click(object sender, EventArgs e)
         {
             try
             {
                 richTextBox1.AppendText(string.Format("{0} {1},{2},", Environment.NewLine, videoDevices[0].MonikerString, comboBoxcameras.SelectedIndex));
                 richTextBox1.AppendText(string.Format("{0} {1},{2},", Environment.NewLine, videoDevices[0].Name, comboBoxcameras.SelectedIndex));
                 var videoSource =
                     new VideoCaptureDevice(videoDevices[comboBoxcameras.SelectedIndex].MonikerString)
                     {
                         DesiredFrameSize = new System.Drawing.Size(320, 240),
                         DesiredFrameRate = 1
                     };
                 richTextBox1.AppendText(string.Format("{0} {1},", Environment.NewLine, 111));
                 videoSourcePlayer.VideoSource = videoSource;
                 richTextBox1.AppendText(string.Format("{0} {1},", Environment.NewLine, 222));
                 videoSourcePlayer.Start();
             }
             catch (Exception ex)
             {
                 richTextBox1.AppendText(Environment.NewLine + ex.Message);
             }
         }

         private void buttoncameraclose_Click(object sender, EventArgs e)
         {
             try
             {
                 videoSourcePlayer.SignalToStop();
                 videoSourcePlayer.WaitForStop();
             }
             catch (Exception ex)
             {
                 richTextBox1.AppendText(string.Format("{0} {1},", Environment.NewLine, ex.Message));
             }
         }

         private void buttoncameracapture_Click(object sender, EventArgs e)
         {
             try
             {
              //   richTextBox1.AppendText(string.Format("{0} {1},", Environment.NewLine, 1111));
                 if (videoSourcePlayer.IsRunning)
                 {
                   //  richTextBox1.AppendText(string.Format("{0} {1},", Environment.NewLine, 2222));
                     BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                     videoSourcePlayer.GetCurrentVideoFrame().GetHbitmap(),
                                     IntPtr.Zero,
                                      Int32Rect.Empty,
                                     BitmapSizeOptions.FromEmptyOptions());
                  //   richTextBox1.AppendText(string.Format("{0} {1},", Environment.NewLine, 3333));
                     PngBitmapEncoder pE = new PngBitmapEncoder();
                  //   richTextBox1.AppendText(string.Format("{0} {1},", Environment.NewLine, 4444));
                     pE.Frames.Add(BitmapFrame.Create(bitmapSource));
                  //   richTextBox1.AppendText(string.Format("{0} {1},", Environment.NewLine, 5555));
                     string picName = Path.GetTempFileName() + ".jpg";
                     //if (File.Exists(picName))
                     //{
                     //    File.Delete(picName);
                     //}
                  //   richTextBox1.AppendText(string.Format("{0} {1},", Environment.NewLine, 6666));
                     using (Stream stream = File.Create(picName))
                     {
                         pE.Save(stream);
                     }
                   //  richTextBox1.AppendText(string.Format("{0} {1},", Environment.NewLine, 7777));
                     //拍照完成后关摄像头并刷新同时关窗体
                     //if (videoSourcePlayer != null && videoSourcePlayer.IsRunning)
                     //{
                     //    videoSourcePlayer.SignalToStop();
                     //    videoSourcePlayer.WaitForStop();
                     //}

                     //  this.Close();
                     textBoxcurrentimage.Text = picName;
                     richTextBox1.AppendText(Environment.NewLine + picName);
                     currentImage = Run(textBoxcurrentimage.Text);
                     pictureBoxcurrentimage.Image = Image.FromFile(currentImage);
                 }
             }
             catch (Exception ex)
             {
                 //  MessageBox.Show("摄像头异常：" + ex.Message);
                 richTextBox1.AppendText(string.Format("{0} {1},", Environment.NewLine, ex.Message));
             }
         }

         private void buttonchecksource_Click(object sender, EventArgs e)
         {
             try
             {
                 sourceImage = Run(textBoxpicturesource.Text);
                 pictureBoxsource.Image = Image.FromFile(sourceImage);
              
             }
             catch (Exception ex)
             {
                 richTextBox1.AppendText(Environment.NewLine + ex.Message);
             }
         }

         private void Form1_Load(object sender, EventArgs e)
         {
             try
             {
                 // 枚举所有视频输入设备

                 videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                 if (videoDevices.Count == 0)
                     throw new ApplicationException();

                 foreach (FilterInfo device in videoDevices)
                 {
                     comboBoxcameras.Items.Add(device.Name);
                 }

                 comboBoxcameras.SelectedIndex = 0;
            

             }
             catch (ApplicationException)
             {
                 comboBoxcameras.Items.Add("No local capture devices");
             }
         }

         private void Form1_FormClosing(object sender, FormClosingEventArgs e)
         {
             try
             {
                 videoSourcePlayer.SignalToStop();
                 videoSourcePlayer.WaitForStop();
                 recognizer.Dispose();
             }
             catch (Exception ex)
             {
              //   richTextBox1.AppendText(string.Format("{0} {1},", Environment.NewLine, ex.Message));
             }
         }

         private void buttontrain_Click(object sender, EventArgs e)
         {
             var countimage = trainingImages.Count;
             var countid = Names_List_ID.Count;
             if (countid != countimage)
             {
                 UpdateStatus(string.Format("not trained {0},{1}", countid,countimage));
                 return;
             }
             recognizer.Train(trainingImages.ToArray(),Names_List_ID.ToArray());
             UpdateStatus(string.Format("trained trained {0},{1}", countid, countimage));
         }

         private void clear_Click(object sender, EventArgs e)
         {
             var countimage = trainingImages.Count;
             var countid = Names_List_ID.Count;
             if (countid != countimage)
             {
                 UpdateStatus(string.Format("not clear {0},{1}", countid, countimage));
                 return;
             }
             trainingImages.Clear();
             Names_List_ID.Clear();
             UpdateStatus(string.Format(" cleared {0},{1}", countid, countimage));
         }

         private void textBoxcurrentimage_TextChanged(object sender, EventArgs e)
         {
             //try
             //{
             //    currentImage = Run(textBoxcurrentimage.Text);
             //    pictureBoxcurrentimage.Image = Image.FromFile(currentImage);
             //}
             //catch (Exception ex)
             //{
                 
             //}
         }
    }
}
