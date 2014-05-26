using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Krisa.Tarjeta.UI
{
    public partial class UIAgregarTarjeta : Form
    {
        /// <summary>
        /// Gestor de la tarjeta
        /// </summary>
        private GestorTarjeta gestor;

        public UIAgregarTarjeta()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Inicializacion del formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIAgregarTarjeta_Load(object sender, EventArgs e)
        {
            cmbDireccion.Items.AddRange(Driver.Tarjetas);
            gestor = new GestorTarjeta();
        }

        /// <summary>
        /// Validacion del nombre de la tarjeta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNombre_Validating(object sender, CancelEventArgs e)
        {
            //Validar campo nombre de la Tarjeta
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                errorAgregarTarjeta.SetErrorResource(txtNombre, "CAMPO_VACIO");
                txtNombre.BackColor = Color.LightSkyBlue;
                e.Cancel = true;
                return;
            }
            if (!Regex.IsMatch(txtNombre.Text, "^[a-zA-Z0-9]*$"))
            {
                errorAgregarTarjeta.SetErrorResource(txtNombre, "CAMPO_LETRASYNUMEROS");
                txtNombre.BackColor = Color.LightSkyBlue;
                e.Cancel = true;
                return;
            }
            e.Cancel = false;
            errorAgregarTarjeta.SetError(txtNombre, "");
            txtNombre.BackColor = SystemColors.Window;
        }

        /// <summary>
        /// Accion del boton Aceptar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    var nombre = txtNombre.Text.Trim();
                    var direccion = (string)cmbDireccion.SelectedItem;

                    gestor.AgregarTarjeta(nombre, direccion);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Accion del boton Cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
