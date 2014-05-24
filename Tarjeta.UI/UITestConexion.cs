using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Krisa.Tarjeta.UI
{
    public partial class UITestConexion : Form
    {
        /// <summary>
        /// Tarjeta abierta
        /// </summary>
        private Tarjeta tarjeta;

        public UITestConexion()
        {
            InitializeComponent();
        }

        private void MainDialog_Load(object sender, EventArgs e)
        {
            try
            {
                cmbTarjeta.Items.AddRange(Driver.Tarjetas);
                if (cmbTarjeta.Items.Count > 0)
                {
                    cmbTarjeta.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbTarjeta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (var tarjeta = new Tarjeta(cmbTarjeta.SelectedItem.ToString()))
                {
                    chkEmulador.Checked = tarjeta.Emulador;
                    lstPuertos.Items.Clear();
                    lstPuertos.Items.AddRange(tarjeta.PuertosDigitales);
                    lstCanales.Items.Clear();
                    lstCanales.Items.AddRange(tarjeta.CanalesDigitales);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lstPuertos_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnLeerPuerto.Enabled = lstPuertos.SelectedItems.Count > 0;
            btnEscribirPuerto.Enabled = lstPuertos.SelectedItems.Count > 0;
        }

        private void lstCanales_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnLeerCanal.Enabled = lstCanales.SelectedItems.Count > 0;
            btnEscribirCanal.Enabled = lstCanales.SelectedItems.Count > 0;
        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            try
            {
                if (tarjeta == null)
                {
                    tarjeta = new Tarjeta(cmbTarjeta.SelectedItem.ToString());
                }

                var canales =
                    sender == btnLeerCanal ?
                    lstCanales.SelectedItems.Cast<string>().ToArray() :
                    lstPuertos.SelectedItems.Cast<string>().ToArray();

                var data = tarjeta.LeerBinario(canales, chkPersistente.Checked);
                txtResult.Text = String.Join(", ", data.Select((b) => b ? "1" : "0"));

                if (chkPersistente.Checked)
                {
                    chkPersistente.Enabled = false;
                    btnParar.Enabled = true;
                }
                else
                {
                    tarjeta.Close();
                    tarjeta = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEscribir_Click(object sender, EventArgs e)
        {
            try
            {
                if (tarjeta == null)
                {
                    tarjeta = new Tarjeta(cmbTarjeta.SelectedItem.ToString());
                }

                var canales =
                    sender == btnEscribirCanal ?
                    lstCanales.SelectedItems.Cast<string>().ToArray() :
                    lstPuertos.SelectedItems.Cast<string>().ToArray();
                var data = txtDatos.Text.Split(',').Select((x) => int.Parse(x)).Select((x) => x != 0).ToArray();

                tarjeta.EscribirBinario(canales, data);

                if (chkPersistente.Checked)
                {
                    chkPersistente.Enabled = false;
                    btnParar.Enabled = true;
                }
                else
                {
                    tarjeta.Close();
                    tarjeta = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnParar_Click(object sender, EventArgs e)
        {
            chkPersistente.Enabled = true;
            btnParar.Enabled = false;
            tarjeta.Close();
            tarjeta = null;
        }

        private void btnAgregarTarjeta_Click(object sender, EventArgs e)
        {
            var form = new UIAgregarTarjeta();
            form.ShowDialog();
        }
    }
}
