
namespace Krisa.Datos
{
    public class VideoCamara
    {
        public int Id { set; get; }
        public string Nombre { set; get; }
        public string Dispositivo { set; get; }
        public bool esActivo { set; get; }

        public override string ToString()
        {
            return Nombre + ", " + Dispositivo;
        }

        /*public override bool Equals(object obj)
        {
            VideoCamara camara = (VideoCamara)obj;
            if (Nombre != camara.Nombre)
            {
                return false;
            }
            else if (Dispositivo != camara.Dispositivo)
            {
                return false;
            }

            return true;
        }

         */
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }


}
