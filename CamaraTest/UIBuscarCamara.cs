using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Video.FFMPEG;
using AForge.Video.VFW;
using System.Drawing.Imaging;


namespace CamaraTest
{
    public partial class UIBuscarCamara : Form
    {

        //Create webcam object
        VideoCaptureDevice videoSource;

        private bool DeviceExist = false;
        private FilterInfoCollection videoDevices;


        public UIBuscarCamara()
        {
            InitializeComponent();
        }

        private void UIBuscarCamara_Load(object sender, EventArgs e)
        {
            getCamList();
            showResolutionsSupported();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                writer.Close();
                label1.Text = "";
            }
            catch (AccessViolationException) { }
            catch (NullReferenceException) { }
            catch (System.IO.IOException) { }
        }


        private void start_Click(object sender, EventArgs e)
        {
            showVideo();
        }

        private string getResolution()
        {
            string r = cb_resolutions.SelectedItem.ToString();
            return r;
        }

        public void showResolutionsSupported()
        {
            FilterInfoCollection videosources = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            videoSource = new VideoCaptureDevice(videosources[0].MonikerString);
            try
            {
                //Check if the video device provides a list of supported resolutions
                if (videoSource.VideoCapabilities.Length > 0)
                {
                    //Search for the highest resolution
                    for (int i = 0; i < videoSource.VideoCapabilities.Length; i++)
                    {
                        cb_resolutions.Items.Add(videoSource.VideoCapabilities[i].FrameSize.Width.ToString() + ";" + i.ToString());
                    }
                    // resolution = cb_resolutions.SelectedItem.ToString();
                    // btn_switchResolution.Click += new EventHandler(switchResolution_Click);
                    //videoSource.VideoResolution = videoSource.VideoCapabilities[Convert.ToInt32(Resolusion.Split(';')[0])];              
                }
            }
            catch { }
        }

        public void showVideo()
        {
            if (start.Text == "&Seleccionar")
            {
                if (DeviceExist)
                {
                    videoSource = new VideoCaptureDevice(videoDevices[comboBox1.SelectedIndex].MonikerString);
                    videoSource.VideoResolution = videoSource.VideoCapabilities[Convert.ToInt32(getResolution().Split(';')[1])];
                    videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                    videoSource.NewFrame += new NewFrameEventHandler(video_NewFrameSave);
                    CloseVideoSource();
                    //videoSource.DesiredFrameSize = new Size(640, 480);
                    //videoSource.DesiredFrameRate = 10;
                    videoSource.Start();
                    label2.Text = "Device running...";
                    start.Text = "&Stop";
                    timer1.Enabled = true;
                }
                else
                {
                    label2.Text = "Error: No Device selected.";
                }
            }
            else
            {
                try
                {
                    if (videoSource.IsRunning)
                    {
                        timer1.Enabled = false;
                        CloseVideoSource();
                        label2.Text = "Device stopped.";
                        start.Text = "&Seleccionar";
                    }
                }
                catch { }

            }

        }

        private void getCamList() //combo agregar camara
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                comboBox1.Items.Clear();
                if (videoDevices.Count == 0)
                    throw new ApplicationException();

                DeviceExist = true;
                foreach (FilterInfo device in videoDevices)
                {
                    comboBox1.Items.Add(device.Name);
                }
                comboBox1.SelectedIndex = 0; //make dafault to first cam
            }
            catch (ApplicationException)
            {
                DeviceExist = false;
                comboBox1.Items.Add("No capture device on your system");
            }
        }

        public void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseVideoSource();
        }

        
        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap img = (Bitmap)eventArgs.Frame.Clone();
            pictureBoxVideo.Image = img;
        }

        Bitmap imgsave;
        private void video_NewFrameSave(object sender, NewFrameEventArgs eventArgs)
        { 
            
            imgsave = (Bitmap)eventArgs.Frame.Clone();
            try
            {
                writer.WriteVideoFrame((Bitmap)eventArgs.Frame.Clone());
            }
            catch (AccessViolationException) { }
            catch (NullReferenceException) { }
            catch (System.IO.IOException) { }

                
        }



        //get total received frame at 1 second tick
        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = "Device running... " + videoSource.FramesReceived.ToString() + " FPS";
        }

        private void CloseVideoSource()
        {
            if (!(videoSource == null))
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource = null;
                }
        }



        private void pictureBoxVideo_Click(object sender, EventArgs e)
        {

        }

        //Falta Cargar los dll  en git
        VideoFileWriter writer;
        private void button2_Click(object sender, EventArgs e)
        {
            writer = new VideoFileWriter();
            writer.Open("VideoGenerado.avi", imgsave.Width, imgsave.Height, 25, VideoCodec.MPEG4);
            label1.Text = "Recording...";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


    }
}
