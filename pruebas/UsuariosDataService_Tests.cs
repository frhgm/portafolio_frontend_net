using app.Data.Implementations;
using classLibrary.DTO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace pruebas
{
    public class UsuariosDataService_Tests
    {
        private UsuariosDataService dataService { get; set; } = null;
        [SetUp]
        public void Setup()
        {
            dataService = new UsuariosDataService();
        }


        [Test]
        public async Task TraerUsuarios_Test()
        {
            // Assign
            List<Usuario> listaUsuarios = new();
            Usuario usuarioUno = new("18392764-7", "Felipe", "Ramirez", "Hites", "framirezhites@maipogrande.cl", 9974303094, 1, "Admin", DateTime.Today, "663401993");
            listaUsuarios.Add(usuarioUno);

            // Act
            var usuariosTraidos = await dataService.TraerUsuarios();
            // Assert 
            Assert.AreEqual(listaUsuarios.Count > 0, usuariosTraidos.usuarios.Count > 0);
        }

        [Test]
        public async Task CrearUsuario_Test(RegistrarUsuario usuarioTest)
        {
            // Assign

            // Act
            var usuariosTraidos = await dataService.TraerUsuarios();
            // Assert 
            //Assert.AreEqual(listaUsuarios.Count > 0, usuariosTraidos.usuarios.Count > 0);
        }
    }
}