using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Krisa.Datos;

namespace Krisa.Camara.Test
{
    [TestClass]
    public class GestorCamaraTest
    {
        [TestMethod]
        public void AgregarCamara()
        {
            string nombre = "Camara Superior";
            string dispositivo = "WebCam de prueba";
           
            GestorCamara gestorCamara = new GestorCamara();

            Assert.IsTrue(gestorCamara.Agregar(nombre, dispositivo));

        }

        [TestMethod]
        public void BuscarCamara()
        {
            string nombre = "Camara Superior";
            string dispositivo = "WebCam de prueba";

            VideoCamara esperado = new VideoCamara();
            esperado.Nombre = nombre;
            esperado.Dispositivo = dispositivo;

            GestorCamara gestorCamara = new GestorCamara();

            Assert.AreEqual(esperado, gestorCamara.Buscar(nombre));
        }


        [TestMethod]
        public void SuspenderCamara()
        {
            string nombre = "Camara Superior";
           
            GestorCamara gestorCamara = new GestorCamara();

            Assert.IsTrue(gestorCamara.Suspender(nombre));

        }

       

    }
}
