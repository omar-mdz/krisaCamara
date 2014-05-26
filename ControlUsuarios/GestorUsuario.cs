using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Krisa.Datos;

namespace Krisa.ControlUsuarios
{
    public class GestorUsuario
    {
        /// <summary>
        /// Agregar un usuario a la Base de Datos
        /// </summary>
        /// <param name="usuario">Usuario que se va a agregar a la base de datos</param 
        /// <returns>true = si el usuario fue agregado con exito, false = ocurrio un error</returns>
        /// <exception cref="Exception">Error de Entity Framework</excepcion>
        /// <exception cref="ApplicationException">El usuario con nombre especificado ya esta registrado</excepcion>
        public bool AgregarUsuario(Usuario usuario)
        {
            if (VerificarUsuario(usuario))
            {
                string contrasenaEncriptada = Encriptar(usuario.Contrasena);
                usuario.Contrasena = contrasenaEncriptada;
                try
                {
                    using (var db = new KrisaDB())
                    {
                        db.Usuarios.Add(usuario);
                        db.SaveChanges();
                        return true;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                throw new ApplicationException(Krisa.Recursos.ERROR_AGREGAR_USUARIO);
            }
        }

        /// <summary>
        /// Modificar la contraseña de un usuario
        /// </summary>
        /// <param name="usuario">Usuario que se va a modificar en la Base de datos</param>
        /// <param name="nuevoPass">Nueva contraseña del usuario</param>
        /// <returns>true = se modifico el usuario, false = ocurrio un error</returns>
        /// <exception cref="Exception">Error de Entity Framework</excepcion>
        /// <exception cref="ApplicationException">La constraseña especificada de usuario no es correcta</excepcion>
       
        public bool ModificarUsuario(Usuario usuario, string nuevaContrasena)
        {   
            string ContrasenaEncriptada = Encriptar(usuario.Contrasena);
            usuario.Contrasena = ContrasenaEncriptada;
            if (ValidarContrasena(usuario))
            {
                try
                {
                    using (var db = new KrisaDB())
                    {
                        string nuevaContrasenaEncriptada = Encriptar(nuevaContrasena);
                        var cambio = from user in db.Usuarios where user.Nombre == usuario.Nombre select user;
                        cambio.First().Contrasena = nuevaContrasenaEncriptada;
                        db.SaveChanges();
                        return true;
                    }
                }
                catch (Exception)
                {
                    throw; 
                }
            }
            else
            {
                throw new ApplicationException(Krisa.Recursos.ERROR_CONTRASENA);
            }
        }

        /// <summary>
        /// Encriptar la contraseña del usuario
        /// </summary>
        /// <param name="contrasena">Contraseña que se va a encriptar con el SHA256</param>
        /// <returns>Cadena de la contraseña encriptada en algoritmo SHA256</returns>
        public string Encriptar(string contrasena)
        {
            SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();

            byte[] inputBytes = Encoding.UTF8.GetBytes(contrasena);
            byte[] hashedBytes = provider.ComputeHash(inputBytes);

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
            {
                output.Append(hashedBytes[i].ToString("x2").ToLower());
            }
            return output.ToString();
        }

        /// <summary>
        /// Validar la contraseña anterior de un usuario
        /// </summary>
        /// <param name="usuario">Usuario para validar su contraseña en la base de datos</param>
        /// <returns>true = contraseña correcta, false = ocurrio un error</returns>
        /// <exception cref="Exception">Error de Entity Framework</excepcion>
        public bool ValidarContrasena(Usuario usuario)
        {
            try
            {
                using (var db = new KrisaDB())
                {
                    var resultado = from user in db.Usuarios where user.Nombre == usuario.Nombre && user.Contrasena == usuario.Contrasena select user;
                    return resultado.Count<Usuario>() == 0 ? false : true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Verificar si un usuario ya esta registrado en la Base de datos
        /// </summary>
        /// <param name="usuario">Usuario que se va verificar si existe en la base de datos</param>
        /// <returns>true = el usuario no esta registrado en la base de datos, false = ocurrio un error</returns>
        /// <exception cref="Exception">Error de Entity Framework</excepcion>
        public bool VerificarUsuario(Usuario usuario)
        {
            try
            {
                using (var db = new KrisaDB())
                {
                    var resultado = from user in db.Usuarios where user.Nombre == usuario.Nombre select user;
                    return resultado.Count<Usuario>() == 0 ? true : false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

