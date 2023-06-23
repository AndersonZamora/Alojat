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
        private readonly IVusuario mVsuario;

        public UsuarioController(IUsuario mUsuario, IRol mRol, IVusuario mVsuario)
        {
            this.mUsuario = mUsuario;
            this.mRol = mRol;
            this.mVsuario = mVsuario;
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
            if (!mVsuario.Validate(usuario,ModelState))
            {
                ViewBag.roles = mRol.ListRoles();

                return View(usuario);
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
            if (!mVsuario.UpdateCate(usuario, ModelState))
            {
                ViewBag.roles = mRol.ListRoles();

                return View(usuario);
            }

            var up = mUsuario.FindUsuario(usuario.UsuarioID);

            mUsuario.UpdateUsuario(usuario);

            return RedirectToAction("Index", "Usuario");
        }

        [Authorize(Roles = "Admin")]
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
        public IActionResult UpdateCont(UserUpdate usuario)
        {
            if(usuario.UsuarioID == 0)    return RedirectToAction("Index", "Usuario");

            if (!mVsuario.UpdatePass(usuario,ModelState))
            {
                ViewBag.roles = mRol.ListRoles();

                var user = mUsuario.FindUsuario(usuario.UsuarioID);

                return View("Editar", user);
            }

            mUsuario.UpdateUsuarioPass(usuario);
            return RedirectToAction("Index", "Usuario");
        }
    }
}
