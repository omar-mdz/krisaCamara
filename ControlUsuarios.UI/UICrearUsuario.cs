using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Krisa.ControlUsuarios;
using Krisa.Datos;

namespace Krisa.ControlUsuarios.UI
{
    public partial class UICrearUsuario : Form
    {
        GestorUsuario gestorUsuario;

        public UICrearUsuario()
        {
            InitializeComponent();
            gestorUsuario = new GestorUsuario();
            txtUsuario.Select();
        }

        /// <summary>
        /// Accion del boton Guardar Usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario();
                usuario.Nombre = txtUsuario.Text;
                usuario.NombreCompleto = txtNombreCompleto.Text;
                usuario.Contrasena = txtPass.Text;
                usuario.Activo = true;

                if (gestorUsuario.AgregarUsuario(usuario))
                {
                    txtUsuario.Text = "";
                    txtNombreCompleto.Text = "";
                    txtPass.Text = "";
                    txtPassConfirmacion.Text = "";

                    MessageBox.Show(Krisa.Recursos.USUARIO_AGREGADO);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Accion del Metodo Cancelar 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Validacion de campo vacio en el Nombre de Usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUsuario_Validating_1(object sender, CancelEventArgs e)
        {
            if (txtUsuario.Text.Trim() == "")
            {
                errorCrearUsuario.SetErrorResource(txtUsuario, "CAMPO_VACIO");
                txtUsuario.BackColor = Color.LightSkyBlue;
                e.Cancel = true;
                return;
            }
            errorCrearUsuario.Clear();
            txtUsuario.BackColor = Color.White;
        }


        /// <summary>
        /// Validacion de campo vacio y que acepte solo letras del Nombre Completo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNombreCompleto_Validating_1(object sender, CancelEventArgs e)
        {
            if (txtNombreCompleto.Text.Trim() == "")
            {
                errorCrearUsuario.SetErrorResource(txtNombreCompleto, "CAMPO_VACIO");
                txtNombreCompleto.BackColor = Color.LightSkyBlue;
                e.Cancel = true;
                return;
            }
            if (!Regex.IsMatch(txtNombreCompleto.Text, "^[A-Z a-z]*$"))
            {
                errorCrearUsuario.SetErrorResource(txtNombreCompleto, "CAMPO_LETRAS");
                txtNombreCompleto.BackColor = Color.LightSkyBlue;
                e.Cancel = true;
                return;
            }
            errorCrearUsuario.Clear();
            txtNombreCompleto.BackColor = Color.White;
        }

        /// <summary>
        /// Validacion de la contraseña no vacio. Longitud minima de 8 caracteres.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPass_Validating_1(object sender, CancelEventArgs e)
        {
            if (txtPass.Text.Trim() == "")
            {
                errorCrearUsuario.SetErrorResource(txtPass, "CAMPO_VACIO");
                txtPass.BackColor = Color.LightSkyBlue;
                e.Cancel = true;
                return;
            }
            if (txtPass.Text.Trim().Length < 8)
            {
                errorCrearUsuario.SetErrorResource(txtPass, "ERROR_CONTRASENA_MINIMA");
                txtPass.BackColor = Color.LightSkyBlue;
                e.Cancel = true;
                return;
            }
            errorCrearUsuario.Clear();
            txtPass.BackColor = Color.White;
        }

        /// <summary>
        /// Validacion de la confirmacion de la contraseña. Campo no vacio.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassConfirmacion_Validating_1(object sender, CancelEventArgs e)
        {
            if (txtPassConfirmacion.Text.Trim() == "")
            {
                errorCrearUsuario.SetErrorResource(txtPassConfirmacion, "CAMPO_VACIO");
                txtPassConfirmacion.BackColor = Color.LightSkyBlue;
                e.Cancel = true;
                return;
            }
            if (txtPass.Text.Trim() != txtPassConfirmacion.Text.Trim())
            {
                errorCrearUsuario.SetErrorResource(txtPassConfirmacion, "ERROR_COINCIDIR_CONTRASENA");
                txtPassConfirmacion.BackColor = Color.LightSkyBlue;
                e.Cancel = true;
                return;
            }
            errorCrearUsuario.Clear();
            txtPassConfirmacion.BackColor = Color.White;
        }
    }
}
