using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Krisa.Tarjeta.Test
{
    public partial class MainDialog : Form
    {
        public MainDialog()
        {
            InitializeComponent();
        }

        private void MainDialog_Load(object sender, EventArgs e)
        {
            try
            {
                cmbTarjeta.Items.AddRange(Driver.Tarjetas);
                cmbTarjeta.SelectedIndex = 0;
            }
            catch (DriverException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbTarjeta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var tarjeta = Driver.AbrirTarjeta(cmbTarjeta.SelectedItem.ToString());
                chkEmulador.Checked = tarjeta.Emulador;
                lstPuertos.Items.Clear();
                lstPuertos.Items.AddRange(tarjeta.PuertosDigitales);
                lstCanales.Items.Clear();
                lstCanales.Items.AddRange(tarjeta.CanalesDigitales);
            }
            catch (DriverException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
