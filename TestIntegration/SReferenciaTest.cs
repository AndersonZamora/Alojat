using Alojat.Models;
using Alojat.service;

namespace TestIntegration
{
    [TestFixture]
    public class SReferenciaTest
    {
        [Test]
        public void DebePoderDevolverUnaListaReferencia()
        {
            var fixture = new SharedDatabaseFixtureTest();

            var context = fixture.CreateContext();

            var service = new SReferencia(context);

            var action = service.LisReferencia();

            Assert.IsNotNull(action);

            Assert.That(action.Count, Is.EqualTo(2));
        }

        [Test]

        public void TestExistsPunto()
        {
            var Id = 1;

            var fixture = new SharedDatabaseFixtureTest();

            var context = fixture.CreateContext();

            var service = new SReferencia(context);

            var action = service.ExistsPunto(Id);

            Assert.IsNotNull(action);
            Assert.IsTrue(action);
        }


        [Test]

        public void TestFindPunto()
        {
            var Id = 1;

            var fixture = new SharedDatabaseFixtureTest();

            var context = fixture.CreateContext();

            var service = new SReferencia(context);

            var action = service.FindPunto(Id);

            Assert.IsNotNull(action);

            Assert.IsInstanceOf<PuntoReferencia>(action);
            Assert.That(action.NombrePuntoReferencia, Is.EqualTo("DIRESA"));
            Assert.That(action.PuntoReferenciaID, Is.EqualTo(Id));
        }

        [Test]

        public void TestFirstPunto()
        {
            var Id = 2;

            var fixture = new SharedDatabaseFixtureTest();

            var context = fixture.CreateContext();

            var service = new SReferencia(context);

            var action = service.FirstPunto(Id);

            Assert.IsNotNull(action);

            Assert.IsInstanceOf<PuntoReferencia>(action);
            Assert.That(action.NombrePuntoReferencia, Is.EqualTo("COMISARIA SAN JOSE"));
            Assert.That(action.PuntoReferenciaID, Is.EqualTo(Id));
        }
    }
}
