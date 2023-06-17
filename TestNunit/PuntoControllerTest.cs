using Alojat.Controllers;
using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestNunit
{
    public class PuntoControllerTest
    {
        [Test]
        public void DebeDevolverUnaLista()
        {
            var mockReferencia = new Mock<IReferencia>();

            List<PuntoReferencia> list = new();

            mockReferencia.Setup(v => v.LisReferencia()).Returns(list);
            var controller = new PuntoController(mockReferencia.Object);

            var events = controller.Index();
            Assert.IsNotNull(events);
            Assert.IsInstanceOf<IActionResult>(events);
            Assert.IsNotInstanceOf<NotFoundResult>(events);
        }

        [Test]
        public void DebeDevolverUnDetalle()
        {
            var mockReferencia = new Mock<IReferencia>();

            List<PuntoReferencia> list = new();

            mockReferencia.Setup(v => v.FirstPunto(1)).Returns(new PuntoReferencia()); ;
            var controller = new PuntoController(mockReferencia.Object);

            var events = controller.Details(1);
            Assert.IsNotNull(events);
            Assert.IsInstanceOf<IActionResult>(events);
            Assert.IsNotInstanceOf<NotFoundResult>(events);
        }

        [Test]
        public void DebePoderCrear()
        {
            var mockReferencia = new Mock<IReferencia>();

            PuntoReferencia punto = new();

            mockReferencia.Setup(v => v.SavePunto(punto));
            var controller = new PuntoController(mockReferencia.Object);

            var events = controller.Create(punto);
            Assert.IsNotNull(events);
            Assert.IsInstanceOf<IActionResult>(events);
            Assert.IsNotInstanceOf<NotFoundResult>(events);
        }

        [Test]
        public void DebePoderViestaEdit()
        {
            var mockReferencia = new Mock<IReferencia>();

            PuntoReferencia punto = new();

            mockReferencia.Setup(v => v.FindPunto(1)).Returns(new PuntoReferencia());
            var controller = new PuntoController(mockReferencia.Object);

            var events = controller.Edit(1);
            Assert.IsNotNull(events);
            Assert.IsInstanceOf<IActionResult>(events);
            Assert.IsNotInstanceOf<NotFoundResult>(events);
        }


        [Test]
        public void DebePoderEditar()
        {
            var mockReferencia = new Mock<IReferencia>();

            PuntoReferencia punto = new();

            mockReferencia.Setup(v => v.FindPunto(1)).Returns(new PuntoReferencia());
            var controller = new PuntoController(mockReferencia.Object);
            controller.ViewData.ModelState.Clear();

            var events = controller.Edit(1, punto);
            Assert.IsNotNull(events);
            Assert.IsInstanceOf<IActionResult>(events);
            Assert.IsNotInstanceOf<NotFoundResult>(events);
        }


        [Test]
        public void DebePoderEliminar()
        {
            var mockReferencia = new Mock<IReferencia>();

            PuntoReferencia punto = new();

            mockReferencia.Setup(v => v.FirstPunto(1)).Returns(new PuntoReferencia());
            var controller = new PuntoController(mockReferencia.Object);
            controller.ViewData.ModelState.Clear();

            var events = controller.Delete(1);
            Assert.IsNotNull(events);
            Assert.IsInstanceOf<IActionResult>(events);
            Assert.IsNotInstanceOf<NotFoundResult>(events);
        }
    }
}
