using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Krisa.Datos
{
    public class UsuarioBD
    {
        private KrisaDB db;

        public UsuarioBD()
        { 
            db = new KrisaDB();
        }

        //Metodo para agregar un usuario
        public void AgregarUsuario(Usuario usuario)
        {
            if (VerificarUsuario(usuario))
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
            }
        }

        //Metood para verificar si un usuario ya este regitrado
        public bool VerificarUsuario(Usuario usuario)
        {
            var resultado = from user in db.Usuarios where user.Nombre_Usuario == usuario.Nombre_Usuario select user;
            return resultado.Count<Usuario>() == 0 ? true: false;
        }

        //Metodo para modificar la contraseña de un usuario
        public void ModificarUsuario(Usuario usuario, string nuevoPass)
        {
            if (ValidarContrasena(usuario.Pass))
            {
                var cambio = from user in db.Usuarios where user.Nombre_Usuario == usuario.Nombre_Usuario select user;

                cambio.First().Pass = nuevoPass;
                db.SaveChanges();
            }
        }

        //Metodo para validar la contraseña anterior de un usuario
        public bool ValidarContrasena(string password)
        {
            var resultado = from user in db.Usuarios where user.Pass == password select user;
            return resultado.Count<Usuario>() == 0 ? false : true;
        }

    }
}
