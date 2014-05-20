using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NationalInstruments.DAQmx;

namespace Krisa.Tarjeta
{
    public class Tarjeta : IDisposable
    {
        /// <summary>
        /// Handle de tarjeta
        /// </summary>
        //private Device device;
        /// <summary>
        /// La lista de las tareas para los puertos y canales
        /// </summary>
        private Dictionary<string, Task> tareas;
        /// <summary>
        /// true - si esta tarjata es un emulador
        /// </summary>
        private bool emulador;
        /// <summary>
        /// Los nombres de los puertos digitales con el numero de los canales
        /// </summary>
        private SortedList<string, int> puertosDigitales;
        /// <summary>
        /// Los nombres de los canales digitales con el numero de las lineas
        /// </summary>
        private SortedList<string, int> canalesDigitales;

        /// <summary>
        /// Crea una nueva instancia de clase Tarjeta
        /// </summary>
        /// <param name="device">Instancia de tarjeta</param>
        internal Tarjeta(Device device)
        {
            //this.device = device;
            // Lista de la tareas
            tareas = new Dictionary<string, Task>();
            try
            {
                // Emulador
                emulador = device.IsSimulated;
                // Canales digitales
                canalesDigitales = new SortedList<string, int>();
                foreach (var canal in device.DILines)
                {
                    canalesDigitales.Add(canal, 1);
                }
                // Puertos digitales
                puertosDigitales = new SortedList<string, int>();
                foreach (var puerto in device.DIPorts)
                {
                    puertosDigitales.Add(puerto, canalesDigitales.Where((x) => x.Key.StartsWith(puerto)).Count());
                }
            }
            catch (DaqException ex)
            {
                throw new DriverException(ex);
            }
        }

        /// <summary>
        /// Crea una nueva instancia de clase Tarjeta
        /// </summary>
        /// <param name="nombre">Nombre de la tarjeta</param>
        /// <exception cref="ArgumentNullException">El nombre de la tarjeta es null</exception>
        /// <exception cref="ArgumentException">El nombre de la tarjeta no esta registrado en el sistema</exception>
        /// <exception cref="DriverException">Error de driver de la tarjeta</exception>
        public Tarjeta(string nombre)
            : this(Driver.AbrirTarjeta(nombre))
        {
        }

        /// <summary>
        /// Regresa si esta tarjeta es un emulador
        /// </summary>
        public bool Emulador
        {
            get
            {
                return emulador;
            }
        }

        /// <summary>
        /// Regresa los nombres de los canales digitales de esta tarjeta
        /// </summary>
        public string[] CanalesDigitales
        {
            get
            {
                return canalesDigitales.Keys.ToArray();
            }
        }

        /// <summary>
        /// Regresa los nombres de los puertos digitales de esta tarjeta
        /// </summary>
        public string[] PuertosDigitales
        {
            get
            {
                return puertosDigitales.Keys.ToArray();
            }
        }

        /// <summary>
        /// Valida los nombres de los puertos y canales
        /// </summary>
        /// <param name="nombres">Nombres de los puertos de entrada/salida</param>
        /// <returns>Numero de canales en los puertos especificados</returns>
        /// <exception cref="ArgumentNullException">Nombres es null</exception>
        /// <exception cref="ArgumentException">
        /// 1. El nombre de canal es null
        /// 2. El nombre de canal no es valido
        /// 3. Se presente el puerto y el canal de mismo puerto
        /// </exception>
        private int ValidarCanales(string[] nombres)
        {
            if (nombres == null)
            {
                throw new ArgumentNullException("nombres");
            }
            if (nombres.Any((x) => x == null))
            {
                throw new ArgumentException("El nombre de canal no puede ser null", "nombres");
            }
            foreach (var nombre in nombres)
            {
                if (!puertosDigitales.ContainsKey(nombre) && !canalesDigitales.ContainsKey(nombre))
                {
                    throw new ArgumentException("El nombre de canal \"" + nombre + "\" no es valido", "nombres");
                }
            }
            var puertos = nombres.Intersect(puertosDigitales.Keys);
            var lineas = 0;
            foreach (var puerto in puertos)
            {
                if (nombres.Intersect(canalesDigitales.Keys).Any((x) => x.StartsWith(puerto)))
                {
                    var canal = nombres.Intersect(canalesDigitales.Keys).Where((x) => x.StartsWith(puerto)).First();
                    throw new ArgumentException("No se puede agregar el puerto \"" + puerto +
                        "\" y el canal \"" + canal + "\" de mismo puerto", "nombres");
                }
                lineas += puertosDigitales[puerto];
            }
            foreach (var canal in nombres.Intersect(canalesDigitales.Keys))
            {
                lineas++;
            }
            return lineas;
        }

        /// <summary>
        /// Lee el valor de un puerto de entrada binario
        /// </summary>
        /// <param name="nombre">Nombre de puerto de entrada</param>
        /// <param name="persistente">true - para crear una tarea de lectura permanente</param>
        /// <returns>El valor de un puerto</returns>
        /// <exception cref="ArgumentNullException">Nombre es null</exception>
        /// <exception cref="ArgumentException">
        /// 1. El nombre de canal no es valido
        /// </exception>
        /// <exception cref="DriverException">Error de driver de la tarjeta</exception>
        public bool[] LeerBinario(string nombre, bool persistente = false)
        {
            if (nombre == null)
            {
                throw new ArgumentNullException("nombre");
            }
            return LeerBinario(new string[] { nombre }, persistente);
        }

        /// <summary>
        /// Lee los valores de unos puertos de entrada binarios
        /// </summary>
        /// <param name="nombres">Nombres de puertos de entrada</param>
        /// <param name="persistente">true - para crear una tarea de lectura permanente</param>
        /// <returns>El valor de los puertos</returns>
        /// <exception cref="ArgumentNullException">Nombres es null</exception>
        /// <exception cref="ArgumentException">
        /// 1. El nombre de canal es null
        /// 2. El nombre de canal no es valido
        /// 3. Se presente el puerto y el canal de mismo puerto
        /// </exception>
        /// <exception cref="DriverException">Error de driver de la tarjeta</exception>
        public bool[] LeerBinario(string[] nombres, bool persistente = false)
        {
            ValidarCanales(nombres);
            Task digitalReadTask = null;
            try
            {
                // Generar nombre de la tarea
                var nombreTarea = "L" + String.Join(",", nombres);
                // Si la tarea esta creada antes
                if (tareas.ContainsKey(nombreTarea))
                {
                    digitalReadTask = tareas[nombreTarea];
                    persistente = true;
                }
                else
                {
                    // Crear nueva tarea
                    digitalReadTask = new Task();
                    // Agregar los canales
                    digitalReadTask.DIChannels.CreateChannel(
                        String.Join(",", nombres),
                        "",
                        ChannelLineGrouping.OneChannelForAllLines);
                    // Guardar tarea persistente
                    if (persistente)
                    {
                        tareas.Add(nombreTarea, digitalReadTask);
                    }
                }
                // Leer los datos
                var reader = new DigitalSingleChannelReader(digitalReadTask.Stream);
                return reader.ReadSingleSampleMultiLine();
            }
            catch (DaqException ex)
            {
                throw new DriverException(ex);
            }
            finally
            {
                if (!persistente && (digitalReadTask != null))
                {
                    digitalReadTask.Dispose();
                    digitalReadTask = null;
                }
            }
        }

        /// <summary>
        /// Escribe el valor de un puerto de salida binario
        /// </summary>
        /// <param name="nombre">Nombre de puerto de salida</param>
        /// <param name="data">Valor para escribir</param>
        /// <param name="persistente">true - para crear una tarea de escritura permanente</param>
        /// <exception cref="ArgumentNullException">
        /// 1. Nombre es null
        /// 2. Data es null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// 1. El nombre de canal no es valido
        /// 2. Numero de canales en data no corrsponde al numero de canales especificados 
        /// </exception>
        /// <exception cref="DriverException">Error de driver de la tarjeta</exception>
        public void EscribirBinario(string nombre, bool[] data, bool persistente = false)
        {
            if (nombre == null)
            {
                throw new ArgumentNullException("nombre");
            }
            EscribirBinario(new string[] { nombre }, data, persistente);
        }

        /// <summary>
        /// Escribe los valores de unos puertos de salida binarios
        /// </summary>
        /// <param name="nombres">Nombres de puertos de salida</param>
        /// <param name="data">Valor para escribir</param>
        /// <param name="persistente">true - para crear una tarea de escritura permanente</param>
        /// <exception cref="ArgumentNullException">
        /// 1. Nombres es null
        /// 2. Data es null
        /// </exception>
        /// <exception cref="ArgumentException">
        /// 1. El nombre de canal es null
        /// 2. El nombre de canal no es valido
        /// 3. Se presente el puerto y el canal de mismo puerto
        /// 4. Numero de canales en data no corrsponde al numero de canales especificados 
        /// </exception>
        /// <exception cref="DriverException">Error de driver de la tarjeta</exception>
        public void EscribirBinario(string[] nombres, bool[] data, bool persistente = false)
        {
            var canales = ValidarCanales(nombres);
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            if (data.Length != canales)
            {
                throw new ArgumentException("El numero de canales especificados " + canales.ToString() +
                    " no corresponde al numero de canales en datos " + data.Length.ToString());
            }
            Task digitalWriteTask = null;
            try
            {
                // Generar nombre de la tarea
                var nombreTarea = "E" + String.Join(",", nombres);
                // Si la tarea esta creada antes
                if (tareas.ContainsKey(nombreTarea))
                {
                    digitalWriteTask = tareas[nombreTarea];
                    persistente = true;
                }
                else
                {
                    // Crear nueva tarea
                    digitalWriteTask = new Task();
                    // Agregar los canales
                    digitalWriteTask.DOChannels.CreateChannel(
                        String.Join(",", nombres),
                        "",
                        ChannelLineGrouping.OneChannelForAllLines);
                    // Guardar tarea persistente
                    if (persistente)
                    {
                        tareas.Add(nombreTarea, digitalWriteTask);
                    }
                }
                // Escribir los datos
                var writer = new DigitalSingleChannelWriter(digitalWriteTask.Stream);
                writer.WriteSingleSampleMultiLine(true, data);
            }
            catch (DaqException ex)
            {
                throw new DriverException(ex);
            }
            finally
            {
                if (!persistente && (digitalWriteTask != null))
                {
                    digitalWriteTask.Dispose();
                    digitalWriteTask = null;
                }
            }
        }

        /// <summary>
        /// Cierra la comunicacion con la tarjeta
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        /// <summary>
        /// Limpia todos los recursos ocupados por esta tarjeta
        /// </summary>
        public void Dispose()
        {
            // Cerrar todas tareas
            foreach (var tarea in tareas)
            {
                tarea.Value.Dispose();
            }
            tareas.Clear();
        }
    }
}
