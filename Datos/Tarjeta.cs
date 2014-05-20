using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Krisa.Datos
{
    /// <summary>
    /// Tarjeta existentes en el laboratorio
    /// </summary>

    public class Tarjeta
    {
        public int ID { get; set; }

        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public string Configuracion { get; set; }

    }

    /// <summary>
    /// Variables utilizas en los Experimentos extraidas de la Tarjeta
    /// </summary>

    public class VariableDatosTarjeta
    {
        public int ID { get; set; }

        public string Nombre { get; set; }

        public string Tipo { get; set; }

        public string Unidad { get; set; }

    }
}
