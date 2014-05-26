using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Krisa.ControlUsuarios.Test
{
    [TestClass]
    public class GestorUsuariosTest
    {
        [TestMethod]
        public void PruebaEncriptar()
        {
            ControlUsuarios.GestorUsuario user = new ControlUsuarios.GestorUsuario();
            string va1 = user.Encriptar("123");
            string va2 = user.Encriptar("  +[plplpl   ");
            string va3 = user.Encriptar(" ");
            string va4 = user.Encriptar("");

            Assert.AreEqual(va1, "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3");
            Assert.AreEqual(va2, "350164e1069432a534978791280985b7bcd1d4668fa91b6cc28066f7d8a05b83");
            Assert.AreEqual(va3, "36a9e7f1c95b82ffb99743e0c5c4ce95d83c9a430aac59f84ef3cbfab6145068");
            Assert.AreNotEqual(va4, "");
        }

        [TestMethod]
        public void GuardarUsuario()
        {
            ControlUsuarios.GestorUsuario user = new ControlUsuarios.GestorUsuario();

            Datos.Usuario usuario = new Datos.Usuario();
            usuario.ID = 23;
            usuario.Nombre = "Demo";
            usuario.NombreCompleto = "Demo Demo Demo";
            usuario.Contrasena = user.Encriptar("MiPasswordSeguro12");
            usuario.Activo = true;

            user.AgregarUsuario(usuario.Nombre, usuario.NombreCompleto, usuario.Contrasena);
            Assert.AreEqual(usuario, true);
        }
    }
}
