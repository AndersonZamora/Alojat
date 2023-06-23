using Alojat.Models;
using Alojat.service;

namespace TestIntegration
{
    [TestFixture]
    public class TestSServicio
    {
        [Test]
        public void DebePoderDevolverUnaLista()
        {
            var fixture = new SharedDatabaseFixtureTest();

            var context = fixture.CreateContext();

            var service = new SServicio(context);
            
            var action = service.ListGet();

            Assert.IsNotNull(action);
            Assert.That(action.Count, Is.EqualTo(1));
        }

        [Test]

        public void TestFirstOrServicio()
        {
            var Id = 1;
            
            var fixture = new SharedDatabaseFixtureTest();

            var context = fixture.CreateContext();

            var service = new SServicio(context);

            var action = service.FirstOr(Id);

            Assert.IsNotNull(action);

            Assert.IsInstanceOf<Servicio>(action);
            Assert.That(action.UbicacionPiso, Is.EqualTo("Primer Piso"));
            Assert.That(action.ServicioID, Is.EqualTo(Id));
        }

        [Test]
        public void TestListGetUser()
        {
            var IdUser = 8;

            var fixture = new SharedDatabaseFixtureTest();

            var context = fixture.CreateContext();

            var service = new SServicio(context);

            var action = service.ListGetUser(IdUser);

            Assert.IsNotNull(action);
            Assert.That(action.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestServicioExists()
        {
            var Id = 1;

            var fixture = new SharedDatabaseFixtureTest();

            var context = fixture.CreateContext();

            var service = new SServicio(context);

            var action = service.ServicioExists(Id);

            Assert.IsNotNull(action);
            Assert.IsTrue(action);
        }

        [Test]
        public void TestServicioExistsNo()
        {
            var Id = 8222;

            var fixture = new SharedDatabaseFixtureTest();

            var context = fixture.CreateContext();

            var service = new SServicio(context);

            var action = service.ServicioExists(Id);

            Assert.IsNotNull(action);
            Assert.IsFalse(action);
        }
    }
}
