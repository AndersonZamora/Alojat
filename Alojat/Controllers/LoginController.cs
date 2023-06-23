using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Alojat.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuario mUsuario;
        private readonly IUserClaim mClaim;
        private readonly IVlogin mVusuario;
        public LoginController(IUsuario mUsuario, IUserClaim mClaim, IVlogin mVusuario)
        {
            this.mUsuario = mUsuario;
            this.mClaim = mClaim;
            this.mVusuario = mVusuario;
        }

        [HttpGet]
        public IActionResult Login()
        {
            try
            {
                if (HttpContext.User.Identity is not ClaimsIdentity identity) return View();

                var user = mClaim.GetUser(identity.Claims);

                if (user.Name != null)
                {
                    return RedirectToAction("Index", "Home");
                }

                mUsuario.Logout(HttpContext);

                return View();
            }
            catch (Exception)
            {
                mUsuario.Logout(HttpContext);
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel usuario)
        {
            if (!mVusuario.Validate(usuario, ModelState))
            {
                return View();
            }

            var usu = mUsuario.ValidarUsuario(usuario.Email, usuario.Password);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usu.Nombres),
                new Claim(ClaimTypes.Email, usu.Email),
                new Claim(ClaimTypes.Role, usu.Rol.DescripcionRol),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(RegistroUsuario usuario)
        {
            if (!mVusuario.Register(usuario, ModelState))
            {
                return View();
            }

            mUsuario.SaveUsuarioRegistro(usuario);
            return RedirectToAction("Login", "Login");
          
        }
    }
}
