using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Krisa.Datos
{

    /// <summary>
    /// Datos obtenidos del Experimento asociado a las variables extraidas de la Tarjeta
    /// </summary>
    class ExperimentoAsociaVariable
    {
        public decimal Valor { get; set;}

        public DateTime FechaDeObtencion { get; set; }

        public VariableDatosTarjeta Owner { get; set; }

        public Experimento Owner { get; set; }
        
    }
}
