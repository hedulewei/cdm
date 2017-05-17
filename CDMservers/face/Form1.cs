using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
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
using Emgu.CV.VideoSurveillance;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

using AForge.Video.DirectShow;
using NHibernate;
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
        private string playpath = string.Empty;
        private string directory = string.Empty;
        VideoCapture  grabber;
        Image<Bgr, Byte> currentFrame; //current image aquired from webcam for display
        Image<Gray, byte> result, TrainedFace = null; //used to store the result image and trained face
        Image<Gray, byte> gray_frame = null; //grayscale current image aquired from webcam for processing
        private int facenum = 0;
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
                var filename = Path.GetTempFileName() + "jpg";
                pictureBoxcurrentimage.Image.Save(filename);
                var res = recognizer.Predict(new Image<Gray,Byte>(filename));
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
         public IList<PersistentImage> GetImages()
         {
             IList<PersistentImage> images = new List<PersistentImage>();
             try
             {
                 ISession session = ImageDatabase.GetCurrentSession();
                 images = session.CreateCriteria(typeof(PersistentImage)).List<PersistentImage>();
                 session.Close();
             }
             catch (Exception ex)
             {
                 richTextBox1.AppendText(Environment.NewLine + ex.Message);
             }

             return images;
         }
         private void Form1_Load(object sender, EventArgs e)
         {
             try
             {
           
            //Initialize the FrameGraber event
           
                Application.Idle += new EventHandler(FrameGrabber);

                 videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                 if (videoDevices.Count == 0)
                     throw new ApplicationException();

                 foreach (FilterInfo device in videoDevices)
                 {
                     comboBoxcameras.Items.Add(device.Name);
                 }

                 comboBoxcameras.SelectedIndex = 0;

                 grabber = new VideoCapture();
                 grabber.QueryFrame();
             }
             catch (ApplicationException)
             {
                 comboBoxcameras.Items.Add("No local capture devices");
             }
         }

         void FrameGrabber(object sender, EventArgs e)
         {
           //  UpdateStatus(string.Format("in grabber standardm,{0}", 111));
             //Get the current frame form capture device
             using (currentFrame = grabber.QueryFrame().ToImage<Bgr, Byte>())
             {
                 if (currentFrame != null)
                 {
                     pictureBoxsource.Image = currentFrame.Bitmap;
                     //   UpdateStatus(string.Format("in grabber standardm,{0}", 333));
                     //   gray_frame = currentFrame.Convert<Gray, Byte>();

                     //var filename = Path.GetTempFileName() + ".jpg";
                     //currentFrame.Save(filename);
                     if (HaveFace(currentFrame))
                     {
                         pictureBoxcurrentimage.Image = currentFrame.Bitmap;
                         UpdateStatus(string.Format("new face founded,{0}", ++facenum));
                         if (facenum > int.Parse(textBoxpicturesource.Text))
                         {
                             Application.Idle -= new EventHandler(FrameGrabber);
                             grabber.Dispose();
                             return;
                         }
                         ISession session = ImageDatabase.GetCurrentSession();
                         ITransaction tx = session.BeginTransaction();

                         try
                         {
                             PersistentImage pImage = new PersistentImage(currentFrame.Width, currentFrame.Height);
                             CvInvoke.cvCopy(currentFrame, pImage, IntPtr.Zero);
                             pImage.DateCreated = DateTime.Now;
                             UpdateStatus(string.Format("add face to sqlite error,{0}", 111));
                             session.Save(pImage);
                             UpdateStatus(string.Format("add face to sqlite error,{0}", 222));
                             tx.Commit();
                         }
                         catch(Exception ex)
                         {
                             UpdateStatus(string.Format("add face to sqlite error,{0}", ex));
                         }

                       
                         session.Close();
                     }
                 }
             }
            ;
           //  UpdateStatus(string.Format("in grabber standardm,{0}", 222));
             //Convert it to Grayscale
            
         }
         bool HaveFace(Image<Bgr,Byte> fname)
         {
             IImage image;

             image = fname;

             long detectionTime;
             List<Rectangle> faces = new List<Rectangle>();
             List<Rectangle> eyes = new List<Rectangle>();
             //   richTextBox1.AppendText(Environment.NewLine + "aaa");
             DetectFace.Detect(
               image, "haarcascade_frontalface_default.xml", "haarcascade_eye.xml",
               faces, eyes,
               out detectionTime);

             switch (faces.Count)
             {
                 case 0:
                     return false;
                 case 1:

                     return true;
                 default:
                     break;
             }
           
             return true;
           
         }
         bool HaveFace(string fname)
         {
             IImage image;

             image = new UMat(fname, ImreadModes.Color);
             //var aaa = new Image<Emgu.CV.Structure.Gray, byte>(fname);
             //trainingImages.Add(aaa);

             //Names_List_ID.Add(Names_List_ID.Count());

             long detectionTime;
             List<Rectangle> faces = new List<Rectangle>();
             List<Rectangle> eyes = new List<Rectangle>();
             //   richTextBox1.AppendText(Environment.NewLine + "aaa");
             DetectFace.Detect(
               image, "haarcascade_frontalface_default.xml", "haarcascade_eye.xml",
               faces, eyes,
               out detectionTime);

             switch (faces.Count)
             {
                 case 0:
                     return false;
                 case 1:
                     
                     return true;
                 default :
                     break;
             }
            //var sizea= faces.Select(a => a.Size).ToArray();
            // Array.Sort(sizea);

            // foreach (Rectangle face in faces)
            // { CvInvoke.Rectangle(image, face, new Bgr(Color.Red).MCvScalar, 2);
            //   //  face.Size
            // }
             return true;
             //    CvInvoke.Rectangle(image, face, new Bgr(Color.Red).MCvScalar, 2);
             //foreach (Rectangle eye in eyes)
             //    CvInvoke.Rectangle(image, eye, new Bgr(Color.Blue).MCvScalar, 2);
             //   richTextBox1.AppendText(Environment.NewLine + "bbb");
             //display the image 
             //using (InputArray iaImage = image.GetInputArray())
             //{
             //    // recognizer.Train(image, );
             //    var tempfile = Path.GetTempFileName() + ".jpg";
             //    //  richTextBox1.AppendText(Environment.NewLine + "ccc");
             //    image.Save(tempfile);
             //    // aaa.Save(tempfile);
             //    // recognizer.Train(image, Names_List_ID.ToArray());
             //    // listmMats.Add(iaImage);
             //    //var a = new ImageViewer();
             //    //a.BackgroundImage = ;
             //    //a.ShowDialog();
             //    //ImageViewer.Show(image, String.Format(
             //    //  //    ImageViewer.Show(image, String.Format(
             //    //    "Completed face and eye detection using {0} in {1} milliseconds",
             //    //    (iaImage.Kind == InputArray.Type.CudaGpuMat && CudaInvoke.HasCuda) ? "CUDA" :
             //    //    (iaImage.IsUMat && CvInvoke.UseOpenCL) ? "OpenCL"
             //    //    : "CPU",
             //    //    detectionTime));
             //    richTextBox1.AppendText(Environment.NewLine + "eee");
             //    return tempfile;
             //}
         }
         private void Form1_FormClosing(object sender, FormClosingEventArgs e)
         {
             try
             {
                 Application.Idle -= new EventHandler(FrameGrabber);
                 grabber.Dispose();
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
             try
             {
                 trainingImages.Clear();
                 Names_List_ID.Clear();

                 ISession session = ImageDatabase.GetCurrentSession();
                 var images = session.CreateCriteria(typeof(PersistentImage)).List<PersistentImage>();
                 for (int i = 1; i <= images.Count; i++)
                 {
                     Image<Bgr, Byte> image = session.Load<PersistentImage>(i);
                     trainingImages.Add(image.Convert<Gray, Byte>());
                     Names_List_ID.Add(i);
                 }
                 session.Close();

                 UpdateStatus(string.Format(" {0} photo for trainin.", images.Count));
             }
             catch (Exception ex)
             {
                 UpdateStatus(string.Format("trained error {0}", ex));
             }
             var success = CdmFaceTrain();
             while (!success)
             {
                 success = CdmFaceTrain(true);
              
             }
            ;
         }

        private bool CdmFaceTrain(bool minus=false)
        {
            try
            {
                if (minus && trainingImages.Count>1)
                {
                    trainingImages.RemoveAt(0);
                    Names_List_ID.RemoveAt(0);
                }
                else if (trainingImages.Count <1)
                {
                    UpdateStatus(string.Format(" {0} ,not trained.", trainingImages.Count));
                    return true;
                }
                UpdateStatus(string.Format(" {0} photo for trainin.", trainingImages.Count));
                recognizer.Train(trainingImages.ToArray(), Names_List_ID.ToArray());

                UpdateStatus(string.Format("trained trained {0}", trainingImages.Count));
                return true;
            }
            catch (Exception ex)
            {
                UpdateStatus(string.Format("trained error {0}", ex));
                return false;
            }
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

         private void openToolStripMenuItem1_Click(object sender, EventArgs e)
         {
             using (OpenFileDialog fileDialog = new OpenFileDialog())
             {
                 fileDialog.Filter = "视频文件(*.avi;*.wmv)|*.avi;*.wmv|(All file(*.*)|*.*";
                 if (fileDialog.ShowDialog() == DialogResult.OK)
                 {
                     //axWindowsMediaPlayer1.SizeMode = PictureBoxSizeMode.Zoom;
                     playpath = fileDialog.FileName;
                     // 初始化视频集合
                     directory = Path.GetDirectoryName(playpath);
                   //  playArray = player.GetplayCollection(directory);
                 }
             }
             axWindowsMediaPlayer1.URL = playpath;
         }

         private void comboBoxcameras_SelectedIndexChanged(object sender, EventArgs e)
         {
             //Application.Idle -= new EventHandler(FrameGrabber);
             //grabber.Dispose();
             //grabber = new VideoCapture(comboBoxcameras.SelectedIndex);
             //grabber.QueryFrame();
         }

         private void buttonswitchcamera_Click(object sender, EventArgs e)
         {
             facenum = 0;
             Application.Idle -= new EventHandler(FrameGrabber);
             grabber.Dispose();
             grabber = new VideoCapture(comboBoxcameras.SelectedIndex);
             grabber.QueryFrame();
             Application.Idle += new EventHandler(FrameGrabber);
         }

         private void buttonstopcapture_Click(object sender, EventArgs e)
         {
             Application.Idle -= new EventHandler(FrameGrabber);
             grabber.Dispose();
         }

         private void buttonloadfaces_Click(object sender, EventArgs e)
         {
             dataGridView1.Rows.Clear();
             IList<PersistentImage> images = GetImages();
             if (images.Count > 0)
             {
                 dataGridView1.Rows.Add(images.Count);
                 for (int i = 0; i < images.Count; i++)
                 {
                     DataGridViewRow row = dataGridView1.Rows[i];
                     row.Cells["idColumn"].Value = images[i].Id; 
                     row.Cells["timeColumn"].Value = images[i].DateCreated;
                     row.Cells["viewColumn"].Value = "View Image";
                 }
             }
         }

         private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
         {
             if (e.ColumnIndex == dataGridView1.Columns["viewColumn"].Index)
             {
                 int imageID = (int)dataGridView1.Rows[e.RowIndex].Cells["idColumn"].Value;
                 ISession session = ImageDatabase.GetCurrentSession();
                 Image<Bgr, Byte> image = session.Load<PersistentImage>(imageID);
                 session.Close();
                 pictureBoxcurrentimage.Image = image.Bitmap;
                 //using (ImageViewer viewer = new ImageViewer())
                 //{
                 //    viewer.Image = image;
                 //    viewer.ShowDialog();
                 //}
             }
         }

         private void buttonclearfaces_Click(object sender, EventArgs e)
         {
             ISession session = ImageDatabase.GetCurrentSession();
             ITransaction tx = session.BeginTransaction();
             session.CreateSQLQuery("delete from Images").ExecuteUpdate();
             tx.Commit();
             session.Close();
             dataGridView1.Rows.Clear();
         }

         private void RefreshGrid()
         {
             throw new NotImplementedException();
         }
    }
}
