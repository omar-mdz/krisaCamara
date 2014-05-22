using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Krisa.Datos;

namespace Krisa.ControlUsuarios
{
    class ControladorUsuarios
    {
        public bool VerificarCuenta(string name)
        { // clase 
            bool verifica = true;
            var context = new KrisaDB();

            var result = from es in context.Usuarios
                         where es.Nombre_Usuario == name
                         select es;

            foreach (var est in result)
            {
                verifica = false;
            }

            return verifica;
        }
    }
}
