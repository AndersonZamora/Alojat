using Alojat.Models;
using Alojat.service;


namespace TestIntegration
{
    [TestFixture]
    public class SCategoriaTest
    {
        [Test]
        public void DebePoderDevolverUnaLista()
        {
            var fixture = new SharedDatabaseFixtureTest();

            var context = fixture.CreateContext();

            var service = new SCategoria(context);

            var action = service.LisCategoria();

            Assert.IsNotNull(action);

            Assert.That(action.Count, Is.EqualTo(2));
        }

        [Test]

        public void TestFindCategoria()
        {
            var Id = 1;

            var fixture = new SharedDatabaseFixtureTest();

            var context = fixture.CreateContext();

            var service = new SCategoria(context);

            var action = service.FindCategoria(Id);

            Assert.IsNotNull(action);

            Assert.IsInstanceOf<Categoria>(action);
            Assert.That(action.NombreCategoria, Is.EqualTo("eliminar"));
            Assert.That(action.CategoriaID, Is.EqualTo(Id));
        }

        [Test]

        public void TestCategoriaExists()
        {
            var Id = 1;

            var fixture = new SharedDatabaseFixtureTest();

            var context = fixture.CreateContext();

            var service = new SCategoria(context);

            var action = service.CategoriaExists(Id);

            Assert.IsNotNull(action);
            Assert.IsTrue(action);
        }
    }

}
