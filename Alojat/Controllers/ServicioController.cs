using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Alojat.Controllers
{
    public class ServicioController : Controller
    {
        private readonly IServicio mServicio;
        private readonly IUsuario mUsuario;
        private readonly IUserClaim mClaim;
        private readonly ICategoria mCategoria;
        private readonly IInmueble mInmueble;
        private readonly ISha mSha;
        private readonly IVservicio mValidate;

        public ServicioController(IServicio mServicio, IUsuario mUsuario, IUserClaim mClaim, ICategoria mCategoria, IInmueble mInmueble, ISha mSha, IVservicio mValidate)
        {
            this.mServicio = mServicio;
            this.mUsuario = mUsuario;
            this.mClaim = mClaim;
            this.mCategoria = mCategoria;
            this.mInmueble = mInmueble;
            this.mSha = mSha;
            this.mValidate = mValidate;
        }

        [Authorize(Roles = "Admin,Propietario")]
        public IActionResult Index()
        {
            try
            {
                if (HttpContext.User.Identity is not ClaimsIdentity identity) return NotFound();

                var data = mClaim.GetUser(identity.Claims);

                var usuario = mUsuario.GetUsuarioRol(data.Email);

                if (usuario.Rol.DescripcionRol == "Admin")
                {
                    return View(mServicio.ListGet());
                }

                return View(mServicio.ListGetUser(usuario.UsuarioID));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin,Propietario")]
        public IActionResult Create()
        {
            try
            {
                if (HttpContext.User.Identity is not ClaimsIdentity identity) return NotFound();

                var data = mClaim.GetUser(identity.Claims);

                var usuario = mUsuario.GetUsuarioRol(data.Email);

                ViewData["CategoriaID"] = new SelectList(mCategoria.LisCategoria(), "CategoriaID", "NombreCategoria");

                if (usuario.Rol.DescripcionRol == "Admin")
                {
                    ViewData["InmuebleID"] = new SelectList(mInmueble.ListInmueRefe(), "InmuebleID", "DireccionInmueble");
                }
                else
                {
                    ViewData["InmuebleID"] = mInmueble.SelectLis(usuario.UsuarioID);
                }

                return View();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin,Propietario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Servicio servicio, IFormFile Imagen)
        {
            try
            {
                if (!mValidate.Validate(servicio, ModelState))
                {
                    ViewData["CategoriaID"] = new SelectList(mCategoria.LisCategoria(), servicio.CategoriaID);
                    ViewData["InmuebleID"] = new SelectList(mInmueble.ListInmueRefe(), "InmuebleID", "DireccionInmueble");
                    return View(servicio);
                }

                if (Imagen != null)
                {
                    Stream image = Imagen.OpenReadStream();
                    var urlimagen = await mSha.SubirStorage(image, Guid.NewGuid().ToString());
                    servicio.ImagenServicio = urlimagen;
                }

                mServicio.SaveServicio(servicio);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin,Propietario")]
        public IActionResult Details(int id)
        {
            try
            {
                var servicio = mServicio.FirstOr(id);

                var cat = new SelectList(mCategoria.LisCategoria(), servicio.CategoriaID);
                var inm = mInmueble.SelectLis(servicio.InmuebleID);

                ViewData["CategoriaID"] = new SelectList(mCategoria.LisCategoria(), "CategoriaID", "NombreCategoria", servicio.CategoriaID);
                ViewData["InmuebleID"] = new SelectList(mInmueble.ListInmueRefe(), "InmuebleID", "DireccionInmueble", servicio.InmuebleID);

                return View(servicio);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin,Propietario")]
        public IActionResult Delete(int id)
        {
            try
            {
                var servicio = mServicio.FirstOr(id);
                mServicio.DeleteServicio(servicio);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin,Propietario")]
        public IActionResult Edit(Servicio servicio)
        {
            try
            {
                if (!mValidate.Validate(servicio, ModelState))
                {
                    ViewData["CategoriaID"] = new SelectList(mCategoria.LisCategoria(), servicio.CategoriaID);
                    ViewData["InmuebleID"] = mInmueble.SelectLis(servicio.InmuebleID);
                    return View(servicio);
                }

                mServicio.UpdateServicio(servicio);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }
    }
}
