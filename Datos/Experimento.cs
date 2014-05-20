using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Krisa.Datos
{
    /// <summary>
    /// Datos del Experimento realizado
    /// </summary>
    class Experimento
    {
        public int ID { get; set; }

        public DateTime FechaHora { get; set; }

        public int Tipo { get; set; }

        public string Observaciones { get; set; }

        public decimal Parametros_Sonido_Frecuencia { get; set; }

        public decimal Parametros_Sonido_Volumen { get; set; }

        public int Parametros_Sonido_Intervalo { get; set; }

        public bool Parametros_Luz { get; set; }

        public int Parametros_NumerosPellets { get; set; }

        public int Parametros_IntervaloParrilla { get; set; }

        
    }
}
