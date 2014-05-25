using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krisa.Datos;
using Krisa.ControlUsuarios;

namespace Krisa.ControlUsuarios.UI
{
    public partial class UIModificarUsuario : Form
    {
        GestorUsuario gestorUsuario;

        public UIModificarUsuario()
        {
            InitializeComponent();
            gestorUsuario = new GestorUsuario();
        }

        /// <summary>
        /// Accion del boton Cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Accion del boton Modificar contraseña de Usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                Usuario usuario = new Usuario();
                usuario.Nombre = txtUsuario.Text;
                usuario.Contrasena = txtPass.Text;
                string nuevaContrasena = txtNuevoPass.Text;
                try
                {
                    if (gestorUsuario.ModificarUsuario(usuario, nuevaContrasena))
                    {
                        txtNuevoPass.Text = "";
                        txtConfirmar.Text = "";
                        txtPass.Text = "";
                        MessageBox.Show(Krisa.Recursos.CONTRASENA_MODIFICADA);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Validacion del campo no vacio de la contraseña de usuario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPass_Validating_1(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPass.Text))
            {
                errorModificarUsuario.SetErrorResource(txtPass, "CAMPO_VACIO");
                txtPass.BackColor = Color.LightSkyBlue;
                e.Cancel = true;
                return;
            }
            e.Cancel = false;
            errorModificarUsuario.SetError(txtPass,"");
            txtPass.BackColor = Color.White;
        }

        /// <summary>
        /// Validacion del campo no vacio y longitud minima de nueva contraseña de 8 caracteres.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNuevoPass_Validating_1(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNuevoPass.Text))
            {
                errorModificarUsuario.SetErrorResource(txtNuevoPass, "CAMPO_VACIO");
                txtNuevoPass.BackColor = Color.LightSkyBlue;
                e.Cancel = true;
                return;
            }
            if (txtNuevoPass.Text.Length < 8)
            {
                errorModificarUsuario.SetErrorResource(txtNuevoPass, "ERROR_CONTRASENA_MINIMA");
                txtNuevoPass.BackColor = Color.LightSkyBlue;
                e.Cancel = true;
                return;
            }
            if (txtPass.Text.Trim() == txtNuevoPass.Text.Trim())
            {
                errorModificarUsuario.SetErrorResource(txtNuevoPass, "ERROR_CONTRASENAS_IGUALES");
                txtNuevoPass.BackColor = Color.LightSkyBlue;
                e.Cancel = true;
                return;
            }
            e.Cancel = false;
            errorModificarUsuario.SetError(txtNuevoPass,"");
            txtNuevoPass.BackColor = Color.White;
        }

        /// <summary>
        /// Validacion de campo de confirmacion de nueva contraseña.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtConfirmar_Validating_1(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmar.Text))
            {
                errorModificarUsuario.SetErrorResource(txtConfirmar, "CAMPO_VACIO");
                txtConfirmar.BackColor = Color.LightSkyBlue;
                e.Cancel = true;
                return;
            }
            if (txtNuevoPass.Text.Trim() != txtConfirmar.Text.Trim())
            {
                errorModificarUsuario.SetErrorResource(txtConfirmar, "ERROR_COINCIDIR_CONTRASENA");
                txtConfirmar.BackColor = Color.LightSkyBlue;
                e.Cancel = true;
                return;
            }
            e.Cancel = false;
            errorModificarUsuario.SetError(txtConfirmar, "");
            txtConfirmar.BackColor = Color.White;
        }

        /// <summary>
        /// No permitir escribir espacios en la contraseña anterior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// No permitir escribir espacios en la nueva contraseña
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNuevoPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// No permitir escribir espacios en la confirmacion de contraseña
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtConfirmar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
