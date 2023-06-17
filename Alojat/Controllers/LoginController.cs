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
        public LoginController(IUsuario mUsuario, IUserClaim mClaim)
        {
            this.mUsuario = mUsuario;
            this.mClaim = mClaim;
        }

        [HttpGet]
        public IActionResult Login()
        {
            try
            {
                if (HttpContext.User.Identity is not ClaimsIdentity identity) return NotFound();

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
        public async Task<IActionResult> Login(Usuario usuario)
        {
            var usu = mUsuario.ValidarUsuario(usuario.Email, usuario.Password);

            if (usu != null)
            {
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

            return View();
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
        public IActionResult Registro(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                mUsuario.SaveUsuarioRegistro(usuario);
                return RedirectToAction("Login", "Login");
            }

            return View();
        }
    }
}
