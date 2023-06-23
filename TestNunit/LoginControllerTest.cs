using Alojat.Controllers;
using Alojat.interfa;
using Alojat.Models;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using System.Security.Claims;

namespace TestNunit
{
    public class LoginControllerTest
    {

        [Test]
        public void ValidarSecion()
        {
            var mockUsuario = new Mock<IUsuario>();
            var mockClaim = new Mock<IUserClaim>();

            var principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Email, "admin@gmail.com"),
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Role, "Admin"),
            }, "Bear Token"));

            var claim = new ClaimsIdent()
            {
                Name = "admin",
                Email = "admin@gmail.com",
                Role = "Admin"
            };

            mockClaim.Setup(m => m.GetUser(principal.Claims)).Returns(claim);

            var controller = new LoginController(mockUsuario.Object, mockClaim.Object, null)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = principal
                    }
                }
            };

            var result = controller.Login() as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task NoDebePoderHacerLogin()
        {
            var mockUsuario = new Mock<IUsuario>();
            var mockClaim = new Mock<IUserClaim>();
            var mockVlogin = new Mock<IVlogin>();

            var login = new LoginModel() 
            { 
                Email = "admin@gmail.com",
                Password = "admin123"
            };

            ModelStateDictionary modelState = new();

            mockVlogin.Setup(m => m.Validate(login, modelState)).Returns(false);

            var controller = new LoginController(mockUsuario.Object, mockClaim.Object, mockVlogin.Object);

            var result = await controller.Login(login);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IActionResult>());
        }

        [Test]
        public async Task DebePoderHacerLogin()
        {
            var mockUsuario = new Mock<IUsuario>();
            var mockClaim = new Mock<IUserClaim>();
            var mockVlogin = new Mock<IVlogin>();

            var login = new LoginModel()
            {
                Email = "admin@gmail.com",
                Password = "admin123"
            };

            var usuario = new Usuario()
            {
                Nombres = "admin",
                Email = "admin@gmail.com",
                Rol = new Rol() { DescripcionRol = "Admin" }
            };

            ModelStateDictionary modelState = new();

            mockVlogin.Setup(m => m.Validate(login, modelState)).Returns(true);
            mockUsuario.Setup(m => m.ValidarUsuario(login.Email, login.Password)).Returns(usuario);

            var principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Email, "admin@gmail.com"),
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Role, "Admin"),
            }, "Bear Token"));

            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock
                .Setup(_ => _.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.FromResult((object)null));

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(_ => _.GetService(typeof(IAuthenticationService)))
                .Returns(authServiceMock.Object);

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(IUrlHelperFactory)))
                .Returns(urlHelperFactory.Object);

            var controller = new LoginController(mockUsuario.Object, mockClaim.Object, mockVlogin.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = principal,
                        RequestServices = serviceProviderMock.Object
                    }
                }
            };

            var result = await controller.Login(login) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.ActionName, Is.EqualTo("Index"));
                Assert.That(result.ControllerName, Is.EqualTo("Home"));
            });
        }

        [Test]
        public async Task Logout()
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock
                .Setup(_ => _.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.FromResult((object)null));

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(_ => _.GetService(typeof(IAuthenticationService)))
                .Returns(authServiceMock.Object);

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(IUrlHelperFactory)))
                .Returns(urlHelperFactory.Object);


            var controller = new LoginController(null, null, null)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        RequestServices = serviceProviderMock.Object
                    }
                }
            };

            var result =  await controller.Logout() as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.ActionName, Is.EqualTo("Login"));
                Assert.That(result.ControllerName, Is.EqualTo("Login"));
            });
        }

        [Test]
        public void DebePoderRegistrar()
        {
            var mockUsuario = new Mock<IUsuario>();
            var mockVusuario = new Mock<IVlogin>();

            RegistroUsuario usuario = new();
            ModelStateDictionary modelState = new();

            mockVusuario.Setup(m => m.Register(usuario, modelState)).Returns(true);
            mockUsuario.Setup(m => m.SaveUsuarioRegistro(usuario));

            var controller = new LoginController(mockUsuario.Object, null, mockVusuario.Object);

            var result = controller.Registro(usuario) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.ActionName, Is.EqualTo("Login"));
                Assert.That(result.ControllerName, Is.EqualTo("Login"));
            });
        }

        [Test]
        public void NoDebePoderRegistrar()
        {
            var mockUsuario = new Mock<IUsuario>();
            var mockVusuario = new Mock<IVlogin>();

            RegistroUsuario usuario = new();
            ModelStateDictionary modelState = new();

            mockVusuario.Setup(m => m.Register(usuario, modelState)).Returns(false);

            var controller = new LoginController(null, null, mockVusuario.Object);

            var result = controller.Registro(usuario);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IActionResult>());
        }
    }
}
