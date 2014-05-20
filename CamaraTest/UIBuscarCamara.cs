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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getCamList();
        }

        private void start_Click(object sender, EventArgs e)
        {
            if (start.Text == "&Start")
            {
                if (DeviceExist)
                {
                    videoSource = new VideoCaptureDevice(videoDevices[comboBox1.SelectedIndex].MonikerString);
                    videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                    CloseVideoSource();
                    videoSource.DesiredFrameSize = new Size(160, 120);
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
                if (videoSource.IsRunning)
                {
                    timer1.Enabled = false;
                    CloseVideoSource();
                    label2.Text = "Device stopped.";
                    start.Text = "&Start";
                }
            }
        }

        private void getCamList()
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

        Bitmap img;
        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            img = (Bitmap)eventArgs.Frame.Clone();
            pictureBoxVideo.Image = img;
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

        private void button2_Click(object sender, EventArgs e)
        {
            int width = 320;
            int height = 240;

            // create instance of video writer 
            VideoFileWriter writer = new VideoFileWriter();
            // create new video file
            writer.Open("choto.avi", width, height, 25, VideoCodec.MPEG4);
            // create a bitmap to save into the video file
            Bitmap image = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            // write 1000 video frames
            for (int i = 0; i < 1000; i++)
            {
                image.SetPixel(i % width, i % height, Color.Red);
                writer.WriteVideoFrame(image);
            }
            writer.Close();
        }
    }
}
