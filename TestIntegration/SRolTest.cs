using Alojat.service;

namespace TestIntegration
{
    [TestFixture]
    public class SRolTest
    {
        [Test]

        public void TestListRoles()
        {
            var fixture = new SharedDatabaseFixtureTest();

            var context = fixture.CreateContext();

            var service = new SRol(context);

            var action = service.ListRoles();

            Assert.IsNotNull(action);
            Assert.That(action.Count, Is.EqualTo(3));
        }
    }
}
