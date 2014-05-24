using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Krisa.Datos
{
    /// <summary>
    /// Los paremetros del experimento
    /// </summary>
    [ComplexType]
    public class ExperimentoParametros
    {
        public ExperimentoParametrosSonido Sonido { get; set; }

        public bool Luz { get; set; }

        public int NumeroPellets { get; set; }

        public long IntervaloParilla { get; set; }
    }
}
