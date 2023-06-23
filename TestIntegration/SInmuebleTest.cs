using Alojat.Models;
using Alojat.service;

namespace TestIntegration
{
    [TestFixture]
    public class SInmuebleTest
    {
        [Test]
        public void DebePoderDevolverUnaListaInmuebles()
        {
            var fixture = new SharedDatabaseFixtureTest();

            var context = fixture.CreateContext();

            var service = new SInmueble(context);

            var action = service.ListInmueRefe();

            Assert.IsNotNull(action);

            Assert.That(action.Count, Is.EqualTo(1));
        }


        [Test]

        public void TestFindInmu()
        {
            var Id = 1;

            var fixture = new SharedDatabaseFixtureTest();

            var context = fixture.CreateContext();

            var service = new SInmueble(context);

            var action = service.FindInmu(Id);

            Assert.IsNotNull(action);

            Assert.IsInstanceOf<Inmueble>(action);
            Assert.That(action.DireccionInmueble, Is.EqualTo("Pasje Primavera"));
            Assert.That(action.InmuebleID, Is.EqualTo(Id));
        }

        [Test]
        public void TestListInmueRefeUsu()
        {
            var UserId = 8;

            var fixture = new SharedDatabaseFixtureTest();

            var context = fixture.CreateContext();

            var service = new SInmueble(context);

            var action = service.ListInmueRefeUsu(UserId);

            Assert.IsNotNull(action);
            Assert.That(action.Count, Is.EqualTo(1));
        }

        [Test]
        public void TesSelectLis()
        {
            var UserId = 8;
            
            var fixture = new SharedDatabaseFixtureTest();

            var context = fixture.CreateContext();

            var service = new SInmueble(context);

            var action = service.SelectLis(UserId);

            Assert.IsNotNull(action);
            Assert.That(action.Count, Is.EqualTo(1));
        }
    }
}
