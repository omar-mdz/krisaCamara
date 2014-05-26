using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Krisa.Camara.UI
{
    public partial class UICamaraTest : Form
    {
        public UICamaraTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GestorCamara gc = new GestorCamara();
            gc.ComenzarGrabacion();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            GestorCamara gc = new GestorCamara();
            gc.DetenerGrabacion();
        }
    }
}
