using Alojat.Controllers;
using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestNunit
{
    public class UsuarioControllerTest
    {
        [Test]
        public void DebeDevolverUnaLista()
        {
            var mockUsuario = new Mock<IUsuario>();

            List<PuntoReferencia> list = new();

            mockUsuario.Setup(v => v.ListUsuarios()).Returns(new List<Usuario>());
            var controller = new UsuarioController(mockUsuario.Object, null);

            var events = controller.Index();
            Assert.IsNotNull(events);
            Assert.IsInstanceOf<IActionResult>(events);
            Assert.IsNotInstanceOf<NotFoundResult>(events);
        }

        [Test]
        public void DebeRegistrar()
        {
            var mockUsuario = new Mock<IUsuario>();

            List<PuntoReferencia> list = new();

            Usuario usuario = new();

            mockUsuario.Setup(v => v.SaveUsuario(usuario));
            var controller = new UsuarioController(mockUsuario.Object, null);
            controller.ViewData.ModelState.Clear();

            var events = controller.Registro(usuario) as RedirectToActionResult;

            Assert.IsNotNull(events);
            Assert.IsInstanceOf<IActionResult>(events);
            Assert.AreEqual("Index", events.ActionName);
            Assert.IsNotInstanceOf<NotFoundResult>(events);
        }

        [Test]
        public void DebeEditar()
        {
            var mockUsuario = new Mock<IUsuario>();

            List<PuntoReferencia> list = new();

            Usuario usuario = new();

            mockUsuario.Setup(v => v.UpdateUsuario(usuario));
            var controller = new UsuarioController(mockUsuario.Object, null);
            controller.ViewData.ModelState.Clear();

            var events = controller.Editar(usuario) as RedirectToActionResult;

            Assert.IsNotNull(events);
            Assert.IsInstanceOf<IActionResult>(events);
            Assert.AreEqual("Index", events.ActionName);
            Assert.IsNotInstanceOf<NotFoundResult>(events);
        }

        [Test]
        public void DebeEliminar()
        {
            var mockUsuario = new Mock<IUsuario>();

            List<PuntoReferencia> list = new();

            Usuario usuario = new() { UsuarioID = 1 };

            mockUsuario.Setup(v => v.FindUsuario(1)).Returns(usuario);
            mockUsuario.Setup(v => v.DeleteUsuario(usuario));
            var controller = new UsuarioController(mockUsuario.Object, null);

            var events = controller.Eliminar(1) as RedirectToActionResult;

            Assert.IsNotNull(events);
            Assert.IsInstanceOf<IActionResult>(events);
            Assert.AreEqual("Index", events.ActionName);
            Assert.IsNotInstanceOf<NotFoundResult>(events);
        }

        [Test]
        public void DebeUpdateContra()
        {
            var mockUsuario = new Mock<IUsuario>();

            List<PuntoReferencia> list = new();

            Usuario usuario = new();

            mockUsuario.Setup(v => v.UpdateUsuario(usuario));
            var controller = new UsuarioController(mockUsuario.Object, null);
            controller.ViewData.ModelState.Clear();

            var events = controller.UpdateCont(usuario) as RedirectToActionResult;

            Assert.IsNotNull(events);
            Assert.IsInstanceOf<IActionResult>(events);
            Assert.AreEqual("Index", events.ActionName);
            Assert.IsNotInstanceOf<NotFoundResult>(events);
        }
    }
}
