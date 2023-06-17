using Alojat.Controllers;
using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.Security.Claims;

namespace TestNunit
{
    public class ServicioControllerTest
    {
        [Test]
        public void DebeDevolverUnaLista()
        {
            var mockClaim = new Mock<IUserClaim>();
            var mockUsuario = new Mock<IUsuario>();
            var mockServicio = new Mock<IServicio>();

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
            mockServicio.Setup(i => i.ListGet()).Returns(new List<Servicio>() { new Servicio() });

            var controller = new ServicioController(mockServicio.Object, mockUsuario.Object, mockClaim.Object, null, null, null, null)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = principal
                    }
                }
            };

            var events = controller.Index() as ViewResult;

            Assert.IsNotNull(events);
            Assert.IsInstanceOf<ViewResult>(events);
            Assert.IsNotInstanceOf<NotFoundResult>(events);
        }

        [Test]
        public void NoDebeDevolverUnaListaNoLogueado()
        {
            var mockClaim = new Mock<IUserClaim>();
            var mockUsuario = new Mock<IUsuario>();
            var mockServicio = new Mock<IServicio>();

            var principal = new ClaimsPrincipal();

            Usuario usuario = new()
            {
                Rol = new Rol() { DescripcionRol = "Admin" },
                Email = "admin@gmail.com",
                Nombres = "admin"
            };

            mockClaim.Setup(c => c.GetUser(principal.Claims)).Returns(new ClaimsIdent() { Email = "admin@gmail.com", Name = "admin", Role = "Admin" });
            mockUsuario.Setup(u => u.GetUsuarioRol("admin@gmail.com")).Returns(usuario);
            mockServicio.Setup(i => i.ListGet()).Returns(new List<Servicio>() { new Servicio() });

            var controller = new ServicioController(mockServicio.Object, mockUsuario.Object, mockClaim.Object, null, null, null, null)
            {
                ControllerContext = new ControllerContext()
            };

            var events = controller.Index() as NotFoundResult; ;

            Assert.IsNotNull(events);
            Assert.AreEqual(404, events.StatusCode);
        }

        [Test]
        public void DebeDevolverViewCreate()
        {
            var mockClaim = new Mock<IUserClaim>();
            var mockUsuario = new Mock<IUsuario>();
            var mockServicio = new Mock<IServicio>();
            var mockCategoria = new Mock<ICategoria>();
            var mockInmueble = new Mock<IInmueble>();


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

            mockClaim.Setup(c => c.GetUser(principal.Claims)).Returns(new ClaimsIdent() { Email = "admin@gmail.com", Name = "admin", Role = "Admin" });
            mockUsuario.Setup(u => u.GetUsuarioRol("admin@gmail.com")).Returns(usuario);
            mockCategoria.Setup(c => c.LisCategoria()).Returns(new List<Categoria>() { new Categoria() });
            mockInmueble.Setup(i => i.ListInmueRefe()).Returns(new List<Inmueble>() { new Inmueble() });
            mockInmueble.Setup(i => i.SelectLis(usuario.UsuarioID)).Returns(new List<SelectListItem>());
            mockServicio.Setup(i => i.ListGet()).Returns(new List<Servicio>() { new Servicio() });

            var controller = new ServicioController(mockServicio.Object, mockUsuario.Object, mockClaim.Object, mockCategoria.Object, mockInmueble.Object, null, null)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = principal
                    }
                }
            };

            var events = controller.Create();

            Assert.IsNotNull(events);
            Assert.IsInstanceOf<IActionResult>(events);
            Assert.IsNotInstanceOf<NotFoundResult>(events);
        }

        [Test]
        public async Task DebePoderCrearUnServico()
        {
            var mockValidate = new Mock<IVservicio>();
            var mockCategoria = new Mock<ICategoria>();
            var mockInmueble = new Mock<IInmueble>();
            var mockServicio = new Mock<IServicio>();

            Usuario usuario = new()
            {
                UsuarioID = 1,
                Rol = new Rol() { DescripcionRol = "Admin" },
                Email = "admin@gmail.com",
                Nombres = "admin"
            };

            Servicio ser = new();
            ModelStateDictionary modelState = new();

            mockValidate.Setup(v => v.Validate(ser, modelState)).Returns(true);
            mockCategoria.Setup(c => c.LisCategoria()).Returns(new List<Categoria>() { new Categoria() });
            mockInmueble.Setup(i => i.ListInmueRefe()).Returns(new List<Inmueble>() { new Inmueble() });
            mockServicio.Setup(i => i.SaveServicio(ser));


            var controller = new ServicioController(mockServicio.Object, null, null, mockCategoria.Object, mockInmueble.Object, null, mockValidate.Object);

            var events = await controller.Create(ser, null) as RedirectToActionResult;

            Assert.IsNotNull(events);
            Assert.AreEqual("Index", events.ActionName);
            Assert.IsInstanceOf<IActionResult>(events);
            Assert.IsNotInstanceOf<NotFoundResult>(events);
        }

        [Test]
        public async Task DebePoderVerDetailDeUnServico()
        {
            var mockServicio = new Mock<IServicio>();
            var mockCategoria = new Mock<ICategoria>();
            var mockInmueble = new Mock<IInmueble>();

            Usuario usuario = new()
            {
                UsuarioID = 1,
                Rol = new Rol() { DescripcionRol = "Admin" },
                Email = "admin@gmail.com",
                Nombres = "admin"
            };

            mockServicio.Setup(i => i.FirstOr(1)).Returns(new Servicio() { InmuebleID = 1, CategoriaID = 1 });
            mockCategoria.Setup(c => c.LisCategoria()).Returns(new List<Categoria>() { new Categoria() });
            mockInmueble.Setup(i => i.SelectLis(1)).Returns(new List<SelectListItem>());
            mockInmueble.Setup(i => i.ListInmueRefe()).Returns(new List<Inmueble>() { new Inmueble() });


            var controller = new ServicioController(mockServicio.Object, null, null, mockCategoria.Object, mockInmueble.Object, null, null);

            var events = controller.Details(1);

            Assert.IsNotNull(events);
            Assert.IsInstanceOf<IActionResult>(events);
            Assert.IsNotInstanceOf<NotFoundResult>(events);
        }

        [Test]
        public async Task DebePoderEliminar()
        {
            var mockServicio = new Mock<IServicio>();
            var mockCategoria = new Mock<ICategoria>();
            var mockInmueble = new Mock<IInmueble>();

            Usuario usuario = new()
            {
                UsuarioID = 1,
                Rol = new Rol() { DescripcionRol = "Admin" },
                Email = "admin@gmail.com",
                Nombres = "admin"
            };

            Servicio ser = new();

            mockServicio.Setup(i => i.FirstOr(1)).Returns(new Servicio() { InmuebleID = 1, CategoriaID = 1 });
            mockServicio.Setup(i => i.DeleteServicio(ser));


            var controller = new ServicioController(mockServicio.Object, null, null, mockCategoria.Object, mockInmueble.Object, null, null);

            var events = controller.Delete(1) as RedirectToActionResult;

            Assert.IsNotNull(events);
            Assert.IsInstanceOf<RedirectToActionResult>(events);
            Assert.AreEqual("Index", events.ActionName);
            Assert.IsNotInstanceOf<NotFoundResult>(events);
        }

        [Test]
        public async Task DebePoderEditar()
        {
            var mockServicio = new Mock<IServicio>();
            var mockCategoria = new Mock<ICategoria>();
            var mockInmueble = new Mock<IInmueble>();
            var mockValidate = new Mock<IVservicio>();

            Usuario usuario = new()
            {
                UsuarioID = 1,
                Rol = new Rol() { DescripcionRol = "Admin" },
                Email = "admin@gmail.com",
                Nombres = "admin"
            };

            Servicio ser = new();
            ModelStateDictionary modelState = new();

            mockCategoria.Setup(c => c.LisCategoria()).Returns(new List<Categoria>() { new Categoria() });
            mockInmueble.Setup(i => i.SelectLis(1)).Returns(new List<SelectListItem>());
            mockValidate.Setup(v => v.Validate(ser, modelState)).Returns(true);


            var controller = new ServicioController(mockServicio.Object, null, null, mockCategoria.Object, mockInmueble.Object, null, mockValidate.Object);

            var events = controller.Edit(ser) as RedirectToActionResult;

            Assert.IsNotNull(events);
            Assert.IsInstanceOf<RedirectToActionResult>(events);
            Assert.AreEqual("Index", events.ActionName);
            Assert.IsNotInstanceOf<NotFoundResult>(events);
        }
    }
}
