using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Krisa.Datos
{
    /// <summary>
    /// Los paremetros del sonido de experimento
    /// </summary>
    [ComplexType]
    public class ExperimentoParametrosSonido
    {
        public float Frequencia { get; set; }

        public float Volumen { get; set; }

        public long Intervalo { get; set; }
    }
}
