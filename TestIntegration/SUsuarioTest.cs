using Alojat.interfa;
using Alojat.Models;
using Alojat.service;
using Moq;

namespace TestIntegration
{
    [TestFixture]
    public class SUsuarioTest
    {
        [Test]
        public void TestFindUsuario()
        {
            var Id = 9;

            var fixture = new SharedDatabaseFixtureTest();

            var mockIsha = new Mock<ISha>();

            var context = fixture.CreateContext();

            var service = new SUsuario(context,mockIsha.Object);

            var action = service.FindUsuario(Id);

            Assert.IsNotNull(action);
            Assert.IsInstanceOf<Usuario>(action);
            Assert.That(action.Nombres, Is.EqualTo("Flor"));
        }

        [Test]
        public void TestFirstOrEmail()
        {
            var email = "flor@gmail.com";

            var fixture = new SharedDatabaseFixtureTest();

            var mockIsha = new Mock<ISha>();

            var context = fixture.CreateContext();

            var service = new SUsuario(context, mockIsha.Object);

            var action = service.FirstOr(email);

            Assert.IsNotNull(action);
            Assert.IsInstanceOf<Usuario>(action);
            Assert.That(action.Email, Is.EqualTo(email));
        }

        [Test]
        public void TestGetUsuarioRol()
        {
            var email = "flor@gmail.com";
            string rol = "Propietario";

            var fixture = new SharedDatabaseFixtureTest();

            var mockIsha = new Mock<ISha>();

            var context = fixture.CreateContext();

            var service = new SUsuario(context, mockIsha.Object);

            var action = service.GetUsuarioRol(email);

            Assert.IsNotNull(action);
            Assert.IsInstanceOf<Usuario>(action);
            Assert.That(rol, Is.EqualTo(action.Rol.DescripcionRol));
        }

        [Test]
        public void TestListUsuarios()
        {
            var Id = 9;
            
            var fixture = new SharedDatabaseFixtureTest();

            var mockIsha = new Mock<ISha>();

            var context = fixture.CreateContext();

            var service = new SUsuario(context, mockIsha.Object);

            var action = service.ListUsuarios();

            Assert.IsNotNull(action);
        }
    }
}
