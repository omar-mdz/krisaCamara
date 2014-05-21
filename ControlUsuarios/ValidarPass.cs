using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Krisa.Datos;

namespace Krisa.ControlUsuarios
{
    class ValidarPass
    {
        public bool VerificarCuenta(string name, string pass)
        { // clase 
            bool verifica = true;
            var context = new KrisaDB();

            Encriptacion encripta = new Encriptacion();
            string hash = encripta.Encriptar(pass);

            var result = from es in context.Usuarios
                         where es.Nombre_Usuario == name && es.Pass == hash
                         select es;

            foreach (var est in result)
            {
                verifica = false;
            }

            return verifica;
        }
    }
}
