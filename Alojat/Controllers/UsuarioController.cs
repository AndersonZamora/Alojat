using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alojat.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsuarioController : Controller
    {
        private readonly IUsuario mUsuario;
        private readonly IRol mRol;

        public UsuarioController(IUsuario mUsuario, IRol mRol)
        {
            this.mUsuario = mUsuario;
            this.mRol = mRol;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            try
            {
                var lstUsuarios = mUsuario.ListUsuarios();

                return View(lstUsuarios);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        public IActionResult Registro()
        {
            ViewBag.roles = mRol.ListRoles();

            return View();
        }

        [HttpPost]
        public IActionResult Registro(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.roles = mRol.ListRoles();

                return View();
            }

            mUsuario.SaveUsuario(usuario);
            return RedirectToAction("Index", "Usuario");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Editar(int ID)
        {
            try
            {
                ViewBag.roles = mRol.ListRoles();

                var usuario = mUsuario.FindUsuario(ID);

                return View(usuario);
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.roles = mRol.ListRoles();

                return View(usuario);
            }

            mUsuario.UpdateUsuario(usuario);

            return RedirectToAction("Index", "Usuario");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Eliminar(int ID)
        {
            try
            {
                var usuario = mUsuario.FindUsuario(ID);

                mUsuario.DeleteUsuario(usuario);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCont(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Usuario");
            }

            mUsuario.UpdateUsuario(usuario);
            return RedirectToAction("Index", "Usuario");
        }
    }
}
