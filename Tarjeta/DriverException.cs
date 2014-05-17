using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NationalInstruments.DAQmx;

namespace Krisa.Tarjeta
{
    public class DriverException : Exception
    {
        internal DriverException(DaqException ex) : base (ex.Message, ex)
        {
        }
    }
}
