using Alojat.Controllers;
using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestNunit
{
    public class BuscarControllerTest
    {
        [Test]
        public void DebeDevolverUnaLista()
        {
            var mockBusquedad = new Mock<IBusquedad>();
            var mockReferencia = new Mock<IReferencia>();
            var mockCategoria = new Mock<ICategoria>();

            mockReferencia.Setup(f => f.LisReferencia()).Returns(new List<PuntoReferencia>());
            mockCategoria.Setup(f => f.LisCategoria()).Returns(new List<Categoria>());

            var controller = new BuscarController(mockBusquedad.Object, mockReferencia.Object, mockCategoria.Object);

            var events = controller.Buscar();
            Assert.IsNotNull(events);
            Assert.IsInstanceOf<ViewResult>(events);
        }

        [Test]
        public async Task DebeDevolverUnaListaBusquedad()
        {
            Buscar buscar = new()
            {
                Tipo = "1",
                Referencia = "3"
            };

            var mockBusquedad = new Mock<IBusquedad>();
            var mockReferencia = new Mock<IReferencia>();
            var mockCategoria = new Mock<ICategoria>();

            mockReferencia.Setup(f => f.LisReferencia()).Returns(new List<PuntoReferencia>());
            mockCategoria.Setup(f => f.LisCategoria()).Returns(new List<Categoria>());
            mockBusquedad.Setup(f => f.CatPun(buscar)).ReturnsAsync(new List<CatPun>() { new CatPun(), new CatPun() });

            var controller = new BuscarController(mockBusquedad.Object, mockReferencia.Object, mockCategoria.Object);

            var events = await controller.Buscar(buscar) as ViewResult;
            Assert.IsNotNull(events);
            Assert.IsInstanceOf<ViewResult>(events);
            Assert.AreEqual(2, events.ViewData.Count);
        }

        [Test]
        public async Task DebeDevolverUnDetalle()
        {

            var mockBusquedad = new Mock<IBusquedad>();
            var mockReferencia = new Mock<IReferencia>();
            var mockCategoria = new Mock<ICategoria>();

            mockReferencia.Setup(f => f.LisReferencia()).Returns(new List<PuntoReferencia>());
            mockCategoria.Setup(f => f.LisCategoria()).Returns(new List<Categoria>());
            mockBusquedad.Setup(f => f.Servicios(1)).ReturnsAsync(new List<ServicioDetail>() { new ServicioDetail() });

            var controller = new BuscarController(mockBusquedad.Object, mockReferencia.Object, mockCategoria.Object);

            var events = await controller.Details(1) as ViewResult;
            Assert.IsNotNull(events);
            Assert.IsInstanceOf<ViewResult>(events);
            Assert.IsInstanceOf<List<ServicioDetail>>(events.Model);
        }
    }
}
