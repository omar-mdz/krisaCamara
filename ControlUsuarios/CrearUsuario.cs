

using Krisa.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace Krisa.ControlUsuarios
{
    public partial class CrearUsuario : Form
    {
        public CrearUsuario()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                if (VerificarCuenta())
                {
                    Guardar();
                    Limpiar();
                    MessageBox.Show("Usuario creado con exito.");
                }
                else
                {
                    MessageBox.Show("El nombre de usuario ya esta registrado.");
                }
            }
        }

        public void Limpiar() {
            txtUsuario.Text = "";
            txtNombreCompleto.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
            txtPass.Text = "";
            txtPassConfirmacion.Text = "";
        }

        public bool ValidarCampos() {
            bool ban = true;

            if (txtUsuario.Text.Trim() == "" || txtNombreCompleto.Text.Trim() == "" || txtTelefono.Text.Trim() == "" || txtEmail.Text.Trim() == "" || txtPass.Text == "" || txtPassConfirmacion.Text == "")
            {
                MessageBox.Show("Todos los campos son obligatorios. Intente de Nuevo.");
                ban = false;
            }
            else
            {
                if (txtPass.Text != txtPassConfirmacion.Text)
                {
                    MessageBox.Show("La contraseña no coincide. Intente de Nuevo.");
                    ban = false;
                }
            }
            return ban;
        }

        public bool VerificarCuenta() {
            bool verifica = true;
            var context = new KrisaDB();

            var result = from es in context.Usuarios
                         where es.Nombre_Usuario == txtUsuario.Text
                         select es;

            foreach (var est in result)
            {
                verifica = false;
            }

            return verifica;
        }

        public void Guardar()
        {
            try
            {
                MD5 md5Hash = MD5.Create();
                string hash = Encriptar(md5Hash, txtPass.Text);

                var context = new KrisaDB();
                var u = new KrisaDB.Usuario()
                {
                    Nombre_Usuario = txtUsuario.Text,
                    Nombre_Completo = txtNombreCompleto.Text,
                    Pass = hash,
                    Telefono = txtTelefono.Text,
                    Email = txtEmail.Text,
                    isActivo = true
                };

                context.Usuarios.Add(u);
                context.SaveChanges();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public string Encriptar(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
