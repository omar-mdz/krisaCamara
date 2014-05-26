using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Krisa.Datos;

namespace Krisa.Camara.UI
{
    public partial class UCSeleccionarCamara : UserControl
    {
        public VideoCamara CamaraSeleccionada { set; get; }
        public int cmbIndex;

        public UCSeleccionarCamara()
        {
            InitializeComponent();
        }

       

        private void cmbCamaras_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIndex = cmbCamaras.SelectedIndex;
            GestorCamara gc = new GestorCamara();
            gc.previsualizarVideo(cmbIndex);
        }

        private void UCSeleccionarCamara_Load(object sender, EventArgs e)
        {
            cmbCamaras.Items.Clear();
            cmbCamaras.Items.AddRange(new GestorCamara().ListarCamaras());
        }
    }
}
