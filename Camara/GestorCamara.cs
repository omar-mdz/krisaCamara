using Krisa.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using AForge.Video.DirectShow;
using AForge.Video.FFMPEG;
using AForge.Video;
using System.Windows.Forms;
using System.Drawing;

namespace Krisa.Camara
{
    public class GestorCamara
    {

        VideoCaptureDevice videoFuente;
        //private bool dispositivoExistente = false;
        private FilterInfoCollection dispositivosDeVideo;

        //Escritor de frames
        static VideoFileWriter writer = new VideoFileWriter();


        public GestorCamara()
        {

        }

        /// <summary>
        /// Agregar una camara a la base de datos local
        /// </summary>
        /// <param name="nombre">Nombre que se le asignará a la cámara ejem (Camara superior)</param>
        /// <param name="dispositivo">Nombre del dispositivo que esta asociado a esta cámara</param>
        /// <returns>true = Si la cámara se agregó con éxito, false = Si ocurrio un error</returns>
        /// <exception cref="ApplicationException">La cámara ya se encuentra en la base de datos</exception>
        /// <exception cref="Exception">Error en Entity Framework</exception>
        public bool Agregar(string nombre, string dispositivo)
        {
            if (!Validar(nombre))
            {
                VideoCamara camara = new VideoCamara();
                camara.Nombre = nombre;
                camara.Dispositivo = dispositivo;
                camara.esActivo = true;
                try
                {
                    using (var db = new KrisaDBCliente())
                    {
                        db.Camaras.Add(camara);
                        db.SaveChanges();
                        return true;
                    }
                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);
                }
            }
            else
            {
                throw new ApplicationException(Krisa.Recursos.ERROR_CAMARA_EXISTE);
            }



        }

        /// <summary>
        /// Buscar una camara por su nombre en la base de datos
        /// </summary>
        /// <param name="nombreCamara">Nombre de la camara a buscar</param>
        /// <returns>Una camara con el nombre especificado</returns>
        public VideoCamara Buscar(string nombreCamara)
        {
            try
            {
                using (var db = new KrisaDBCliente())
                {
                    var resultado = from cam in db.Camaras
                                    where cam.Nombre == nombreCamara
                                    select cam;
                    return resultado.First();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        private bool Validar(string nombreCamara)
        {
            try
            {
                using (var db = new KrisaDBCliente())
                {
                    var resultado = from cam in db.Camaras
                                    where cam.Nombre == nombreCamara
                                    select cam;
                    return resultado.Any();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Suspender una camara en la base de datos
        /// </summary>
        /// <param name="nombreCamara">Nombre de la camara a suspender</param>
        /// <returns>true = Si la cámara se suspendió con éxito, false = Si ocurrió un error</returns>
        public bool Suspender(string nombreCamara)
        {
            try
            {
                using (var db = new KrisaDBCliente())
                {
                    var suspender = from cam in db.Camaras
                                    where cam.Nombre == nombreCamara
                                    select cam;
                    suspender.Single().esActivo = false;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Listar todas las cámaras de video que se encuentran conectadas al sistema.
        /// </summary>
        /// <returns>Cámaras conectadas y reconocidas por el sistema</returns>
        public Object[] ListarCamaras()
        {
            List<Object> dispositivos = new List<Object>();
            try
            {
                dispositivosDeVideo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                //cmbCamaras.Items.Clear();
                if (dispositivosDeVideo.Count == 0)
                    throw new ApplicationException();

                //bool dispositivoExistente = true;
                //int i = 0;
                foreach (FilterInfo dispositivo in dispositivosDeVideo)
                {
                    //cmbCamaras.Items.Add(dispositivo.Name);
                    //dispositivos[i] = dispositivo.Name;
                    //i++;
                    dispositivos.Add(dispositivo.Name);
                }
                return dispositivos.ToArray();
                //cmbCamaras.SelectedIndex = 0; //Pone a la primera cámara como default
            }
            catch (ApplicationException)
            {
                //dispositivoExistente = false;
                throw new ApplicationException("No existen dispositivos de caputra en el sistema");
            }
        }

        /// <summary>
        /// Obtener las cámaras que están registradas en la base de datos
        /// </summary>
        /// <returns>Cámaras registradas en la base de datos</returns>
        public Object[] ObtenerCamaras()
        {
            try
            {
                using (var db = new KrisaDBCliente())
                {
                    var suspender = from cam in db.Camaras
                                    select cam;
                    return suspender.ToArray();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        ////////////ACCIONES VIDEO//////////////////


        /// <summary>
        /// Permite previsualizar el video antes de iniciar a grabar.
        /// </summary>
        /// <param name="indiceCamara">Recibe un dispositivo de video</param>
        public void previsualizarVideo(int indiceCamara)
        {
            //videoFuente.VideoResolution = videoFuente.VideoCapabilities[Convert.ToInt32("640;480")];
            try
            {
                FilterInfoCollection coleccionDeCamaras = obtenerColeccionDeCamaras();
                videoFuente = new VideoCaptureDevice(coleccionDeCamaras[indiceCamara].MonikerString);

                videoFuente.NewFrame += new NewFrameEventHandler(VideoNewFrame);
                videoFuente.NewFrame += new NewFrameEventHandler(VideoNewFrameSave);
                CerrarConexion();
                videoFuente.Start();
            }
            catch (Exception)
            {

                throw new ApplicationException("Previsualizar video");
            }

        }

        private void CerrarConexion()
        {
            if (!(videoFuente == null))
                if (videoFuente.IsRunning)
                {
                    videoFuente.SignalToStop();
                    videoFuente = null;
                }
        }

        public FilterInfoCollection obtenerColeccionDeCamaras()
        {
            return dispositivosDeVideo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        }


        public static Bitmap img;
        public static bool existeNuevoFrame { set; get; }

        public void VideoNewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            existeNuevoFrame = true;
            img = (Bitmap)eventArgs.Frame.Clone();


        }

        static Bitmap imgsave;
        public void VideoNewFrameSave(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                imgsave = (Bitmap)eventArgs.Frame.Clone();
                writer.WriteVideoFrame((Bitmap)eventArgs.Frame.Clone());
            }
            catch (Exception) { }


        }

        public bool getExisteNuevoFrame()
        {
            return existeNuevoFrame;
        }

        public Bitmap getBitmap()
        {
            existeNuevoFrame = false;
            return img;
        }



        public void ComenzarGrabacion()
        {
            try
            {
                writer.Open("VideoGenerado.avi", imgsave.Width, imgsave.Height, 22, VideoCodec.MPEG4);

            }
            catch (Exception) { }

        }

        public void DetenerGrabacion()
        {
            try
            {
                writer.Close();
            }
            catch (Exception) { }
        }

    }
}
