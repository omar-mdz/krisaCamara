

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

        private void Button1_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                if (verificarCuenta())
                {
                    guardar();
                    limpiar();
                    MessageBox.Show("Usuario insertado con exito");
                }
                else
                {
                    MessageBox.Show("El nombre del usuario ya esta registrado, ocupe otro");
                }
            }
        }

        public void limpiar() {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
        }

        public Boolean validarCampos() {
            Boolean ban = true;
            if (TextBox1.Text == "") {
                MessageBox.Show("El campo de usuario esta vacio");
                ban = false;
            }
            if (TextBox2.Text == "")
            {
                MessageBox.Show("El campo de nombre completo esta vacio");
                ban = false;
            }
            if (TextBox3.Text == "")
            {
                MessageBox.Show("El campo de contraseña esta vacio");
                ban = false;
            }
            if (TextBox4.Text == "")
            {
                MessageBox.Show("El campo de confirmar contraseña esta vacio");
                ban = false;
            }

            if (TextBox4.Text != TextBox3.Text) {
                MessageBox.Show("Las contraseñas no coinciden");
                ban = false;
            }

            return ban;
        }

        public Boolean verificarCuenta() {
            Boolean verifica = true;
            var context = new KrisaDB();

            var result = from es in context.Usuarios
                         where es.Nusuario == TextBox1.Text
                         select es;

            foreach (var est in result)
            {
                verifica = false;
            }

            return verifica;
        }

        public void guardar()
        {
            try
            {
                MD5 md5Hash = MD5.Create();
                string hash = encriptar(md5Hash, TextBox3.Text);

                var context = new KrisaDB();
                var u = new KrisaDB.Usuario()
                {
                    Nusuario = TextBox1.Text,
                    Nombre = TextBox2.Text,
                    Pass = hash
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

        public string encriptar(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
           
        }            
    }
}
