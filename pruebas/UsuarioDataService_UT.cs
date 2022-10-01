using app.Data.Implementations;
using NUnit.Framework;

namespace pruebas
{
    public class UsuariosDataService_UT
    {
        UsuariosDataService us;
        [SetUp]
        public void Setup()
        {
            us = new UsuariosDataService();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void TestLogin(string rut, string pass)
        {

        }
    }
}