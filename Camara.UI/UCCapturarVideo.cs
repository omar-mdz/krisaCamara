using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Krisa.Camara;

namespace Krisa.Camara.UI
{
    public partial class UCCapturarVideo : UserControl
    {
        GestorCamara gc;
        public string camara { set; get; }
        public bool iniciarVideo { get; set; }

        public UCCapturarVideo()
        {
            InitializeComponent();
            try
            {
                gc = new GestorCamara();
            }
            catch (Exception) { }
            
        }

        private void actualizaFrame(object sender, EventArgs e)
        {
           
        }

        private void UCCapturarVideo_Load(object sender, EventArgs e)
        {

        }

        private void actualizarVideoFrame(object sender, EventArgs e)
        {
            while (gc.getExisteNuevoFrame())
            {
                pcbVideo.Image = gc.getBitmap();
            }
        }
    }
}
