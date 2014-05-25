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
            VideoCamara camara = new VideoCamara();
            camara.Nombre = "Camara de prueba";
            camara.Dispositivo = "Dispositivo de prueba";
            camara.esActivo = true;

            CamaraDB camaraDB = new CamaraDB();
            camaraDB.Agregar(camara);


        }

        [TestMethod]
        public void BuscarCamara()
        {
            CamaraDB camaraDB = new CamaraDB();
            VideoCamara camara = new VideoCamara();
            camara.Nombre = "Camara de prueba";
            camara.Dispositivo = "Dispositivo de prueba";
            camara.esActivo = true;

            VideoCamara camaraBuscada = new VideoCamara();
            camaraBuscada = camaraDB.Buscar("Camara de prueba");
            Assert.AreEqual(camara, camaraBuscada);
        }

        [TestMethod]
        public void SuspenderCamara()
        {
             CamaraDB camaraDB = new CamaraDB();
            VideoCamara camara = new VideoCamara();
            camara.Nombre = "Camara de prueba";
            camara.Dispositivo = "Dispositivo de prueba";
            camara.esActivo = true;

            camaraDB.Suspender(camara);

        }

    }
}
