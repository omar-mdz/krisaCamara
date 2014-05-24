using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Krisa.Datos
{
    /// <summary>
    /// Los parametros de un usuario
    /// </summary>
    public class Usuario
    {
        public int ID { get; set; }

        public string Nombre { get; set; }

        public string NombreCompleto { get; set; }

        public string Contrasena { get; set; }

        public bool Activo { get; set; }
    }
}
