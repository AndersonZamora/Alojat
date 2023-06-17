using Alojat.Controllers;
using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using System.Security.Claims;

namespace TestNunit
{
    public class InmuebleControllerTest
    {
        [Test]
        public void DebeDevolverUnaLista()
        {
            var mockClaim = new Mock<IUserClaim>();
            var mockUsuario = new Mock<IUsuario>();
            var mockInmueble = new Mock<IInmueble>();

            var principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Email, "admin@gmail.com"),
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Role, "Admin"),
            }, "Bear Token"));

            Usuario usuario = new()
            {
                Rol = new Rol() { DescripcionRol = "Admin" },
                Email = "admin@gmail.com",
                Nombres = "admin"
            };

            mockClaim.Setup(c => c.GetUser(principal.Claims)).Returns(new ClaimsIdent() { Email = "admin@gmail.com", Name = "admin", Role = "Admin" });
            mockUsuario.Setup(u => u.GetUsuarioRol("admin@gmail.com")).Returns(usuario);
            mockInmueble.Setup(i => i.ListInmueRefe()).Returns(new List<Inmueble>() { new Inmueble() });

            var controller = new InmuebleController(mockInmueble.Object, mockUsuario.Object, mockClaim.Object, null, null, null)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = principal
                    }
                }
            };

            var events = controller.Index();

            Assert.IsNotNull(events);
            Assert.IsInstanceOf<IActionResult>(events);
        }

        [Test]
        public void NoDebeDevolverUnaListaNoLogueado()
        {
            var mockClaim = new Mock<IUserClaim>();
            var mockUsuario = new Mock<IUsuario>();
            var mockInmueble = new Mock<IInmueble>();

            var principal = new ClaimsPrincipal();

            Usuario usuario = new()
            {
                Rol = new Rol() { DescripcionRol = "Admin" },
                Email = "admin@gmail.com",
                Nombres = "admin"
            };

            mockClaim.Setup(c => c.GetUser(principal.Claims)).Returns(new ClaimsIdent() { Email = "admin@gmail.com", Name = "admin", Role = "Admin" });
            mockUsuario.Setup(u => u.GetUsuarioRol("admin@gmail.com")).Returns(usuario);
            mockInmueble.Setup(i => i.ListInmueRefe()).Returns(new List<Inmueble>() { new Inmueble() });

            var controller = new InmuebleController(mockInmueble.Object, mockUsuario.Object, mockClaim.Object, null, null, null);

            var events = controller.Index() as NotFoundResult; ;

            Assert.IsNotNull(events);
            Assert.AreEqual(404, events.StatusCode);
        }

        [Test]
        public void DebeDevolverViewCreate()
        {

            var mockIReferencia = new Mock<IReferencia>();

            mockIReferencia.Setup(c => c.ListReferen()).Returns(new List<PuntoReferencia>() { new PuntoReferencia() });

            var controller = new InmuebleController(null, null, null, mockIReferencia.Object, null, null);

            var events = controller.Create() as ViewResult;

            Assert.IsNotNull(events);
            Assert.IsInstanceOf<ViewResult>(events);
        }

        [Test]
        public async Task DebePoderCrearUnImbueble()
        {
            var principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Email, "admin@gmail.com"),
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Role, "Admin"),
            }, "Bear Token"));

            Usuario usuario = new()
            {
                UsuarioID = 1,
                Rol = new Rol() { DescripcionRol = "Admin" },
                Email = "admin@gmail.com",
                Nombres = "admin"
            };

            var mockIReferencia = new Mock<IReferencia>();
            var mockClaim = new Mock<IUserClaim>();
            var mockUsuario = new Mock<IUsuario>();
            var mockInmueble = new Mock<IInmueble>();
            var mockValidate = new Mock<IVinmueble>();

            Inmueble inmueble = new();
            //var stream = new MemoryStream();

            ModelStateDictionary modelState = new();

            mockIReferencia.Setup(c => c.ListReferen()).Returns(new List<PuntoReferencia>() { new PuntoReferencia() });
            mockClaim.Setup(c => c.GetUser(principal.Claims)).Returns(new ClaimsIdent() { Email = "admin@gmail.com", Name = "admin", Role = "Admin" });
            mockUsuario.Setup(u => u.FirstOr("admin@gmail.com")).Returns(usuario);
            mockValidate.Setup(v => v.Validate(inmueble, modelState)).Returns(true);
            mockInmueble.Setup(i => i.SaveInmueble(inmueble));
            //IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", "imagen.png");

            var controller = new InmuebleController(mockInmueble.Object, mockUsuario.Object, mockClaim.Object, mockIReferencia.Object, mockValidate.Object, null)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = principal
                    }
                }
            };

            var events = await controller.Create(inmueble, null) as RedirectToActionResult;

            Assert.IsNotNull(events);
            Assert.AreEqual("Index", events.ActionName);
        }

        [Test]
        public void DebeDevolverViewDetails()
        {

            var mockInmueble = new Mock<IInmueble>();
            var mockIReferencia = new Mock<IReferencia>();

            mockIReferencia.Setup(c => c.ListReferen()).Returns(new List<PuntoReferencia>() { new PuntoReferencia() });
            mockInmueble.Setup(c => c.FindInmu(1)).Returns(new Inmueble());

            var controller = new InmuebleController(mockInmueble.Object, null, null, mockIReferencia.Object, null, null);

            var events = controller.Details(1);

            Assert.IsNotNull(events);
            Assert.IsInstanceOf<IActionResult>(events);
            Assert.IsNotInstanceOf<RedirectToRouteResult>(events);
        }

        [Test]
        public void DebePoderEliminar()
        {

            var mockInmueble = new Mock<IInmueble>();
            var mockIReferencia = new Mock<IReferencia>();

            Inmueble inmueble = new();

            mockInmueble.Setup(c => c.FindInmu(1)).Returns(inmueble);
            mockInmueble.Setup(c => c.RemoveInmueble(inmueble));

            var controller = new InmuebleController(mockInmueble.Object, null, null, mockIReferencia.Object, null, null);

            var events = controller.Delete(1) as RedirectToActionResult;

            Assert.IsNotNull(events);
            Assert.IsInstanceOf<RedirectToActionResult>(events);
            Assert.AreEqual("Index", events.ActionName);
        }

        [Test]
        public void DebePoderEditar()
        {

            var mockInmueble = new Mock<IInmueble>();
            var mockValidate = new Mock<IVinmueble>();

            ModelStateDictionary modelState = new();
            Inmueble inmueble = new();

            mockInmueble.Setup(c => c.UpdateInmueble(inmueble));
            mockValidate.Setup(v => v.Validate(inmueble, modelState)).Returns(true);

            var controller = new InmuebleController(mockInmueble.Object, null, null, null, mockValidate.Object, null);

            var events = controller.Edit(inmueble) as RedirectToActionResult;

            Assert.IsNotNull(events);
            Assert.IsInstanceOf<RedirectToActionResult>(events);
            Assert.AreEqual("Index", events.ActionName);
        }
    }
}
