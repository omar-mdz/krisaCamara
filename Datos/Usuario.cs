using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Krisa.Datos
{
    /// <summary>
    /// Los paremetros de un usuario
    /// </summary>
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombre_Usuario { get; set; }
        public string Nombre_Completo { get; set; }
        public string Pass { get; set; }
        public bool isActivo { get; set; }
    }
}
