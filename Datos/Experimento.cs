using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Krisa.Datos
{
    /// <summary>
    /// El tipo de experimento
    /// </summary>
    public enum ExperimentoTipo : int
    {
        Entrenamiento,
        Seguro,
        Riesgo
    }

    /// <summary>
    /// Datos del Experimento realizado
    /// </summary>
    public class Experimento
    {
        public int ID { get; set; }

        public DateTime FechaHora { get; set; }

        public ExperimentoTipo Tipo { get; set; }
        
        public string Observaciones { get; set; }
        
        public ExperimentoParametros Parametros { get; set; }
    }
}
