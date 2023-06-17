using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Alojat.Controllers
{
    public class InmuebleController : Controller
    {
        private readonly IInmueble mInmueble;
        private readonly IUsuario mUsuario;
        private readonly IUserClaim mClaim;
        private readonly IReferencia mReferencia;
        private readonly IVinmueble mValidate;
        private readonly ISha mSha;

        public InmuebleController(IInmueble mInmueble, IUsuario mUsuario, IUserClaim mClaim, IReferencia mReferencia, IVinmueble mValidate, ISha mSha)
        {
            this.mInmueble = mInmueble;
            this.mUsuario = mUsuario;
            this.mClaim = mClaim;
            this.mReferencia = mReferencia;
            this.mValidate = mValidate;
            this.mSha = mSha;
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
                    return View(mInmueble.ListInmueRefe());
                }

                return View(mInmueble.ListInmueRefeUsu(usuario.UsuarioID));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin,Propietario")]
        public ViewResult Create()
        {
            ViewData["PuntoReferencia"] = new SelectList(mReferencia.ListReferen(), "PuntoReferenciaID", "NombrePuntoReferencia");
            return View();
        }

        [Authorize(Roles = "Admin,Propietario")]
        [HttpPost]
        public async Task<IActionResult> Create(Inmueble inmueble, IFormFile Imagen)
        {
            try
            {
                if (HttpContext.User.Identity is not ClaimsIdentity identity) return NotFound();

                var data = mClaim.GetUser(identity.Claims);

                var usuario = mUsuario.FirstOr(data.Email);

                if (!mValidate.Validate(inmueble, ModelState))
                {
                    ViewData["PuntoReferencia"] = new SelectList(mReferencia.ListReferen(), "PuntoReferenciaID", "NombrePuntoReferencia", inmueble.PuntoReferenciaID);
                    return View("Create", inmueble);
                }

                if (Imagen != null)
                {
                    Stream image = Imagen.OpenReadStream();

                    var urlimagen = await mSha.SubirStorage(image, Guid.NewGuid().ToString());
                    inmueble.ImagenInmueble = urlimagen;
                }

                inmueble.UsuarioID = usuario.UsuarioID;

                mInmueble.SaveInmueble(inmueble);

                return RedirectToAction("Index");
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
                var inmueble = mInmueble.FindInmu(id);

                ViewData["PuntoReferenciaID"] = new SelectList(mReferencia.ListReferen(), "PuntoReferenciaID", "NombrePuntoReferencia", inmueble.PuntoReferenciaID);

                return View(inmueble);
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
                var inmueble = mInmueble.FindInmu(id);
                mInmueble.RemoveInmueble(inmueble);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin,Propietario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Inmueble inmueble)
        {
            try
            {
                if (!mValidate.Validate(inmueble, ModelState))
                {
                    return RedirectToAction(nameof(Index));
                }

                mInmueble.UpdateInmueble(inmueble);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}