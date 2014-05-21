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
using System.Data.Entity;
	
//using Control_Usuarios.Delegados;

namespace Krisa.ControlUsuarios
{
    public delegate void VisibilidadFormulario(bool abolFormulario);
    public partial class UIModificar_Usuario : Form
    {
        public VisibilidadFormulario pDelVisibilidadFormulario;

        public UIModificar_Usuario()
        {
            InitializeComponent();
        }

        private void frmControlUsuarioModificar_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (pDelVisibilidadFormulario != null)
            {
                pDelVisibilidadFormulario(true);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.CerrarVentana();
        }

        private void CerrarVentana()
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (txtUsuario.Text.Trim() != "" && txtPass.Text.Trim() != "" && txtConfirmar.Text.Trim() != "" && txtNuevoPass.Text.Trim() != "")
            {
                ValidarPass valida = new ValidarPass();

                if (valida.VerificarCuenta(txtUsuario.Text, txtUsuario.Text))
                {
                    if (txtNuevoPass.Text.Equals(txtConfirmar.Text))
                    {
                        var context = new KrisaDB();

                        Encriptacion encripta = new Encriptacion();
                        string hash = encripta.Encriptar(txtPass.Text);
                        string hashNuevo = encripta.Encriptar(txtNuevoPass.Text);

                        var cambio = from es in context.Usuarios
                                     where es.Nombre_Usuario == txtUsuario.Text && es.Pass == hash
                                     select es;
       
                        cambio.First().Pass=hashNuevo;
                        context.SaveChanges();
                        context.Dispose();

                        MessageBox.Show("Su contraseña fue modificada con exito");

                        txtPass.Text = "";
                        txtNuevoPass.Text = "";
                        txtConfirmar.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("La nueva contraseña no coincide");
                    }
                }
                else
                {
                    MessageBox.Show("La contraseña es incorrecta");
                }
            }
            else
            {
                MessageBox.Show("Todos los campos son obligatorios. Intente de nuevo");
            }
        }
         
    }
}
