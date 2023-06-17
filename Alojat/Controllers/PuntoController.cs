using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alojat.Controllers
{
    public class PuntoController : Controller
    {
        private readonly IReferencia mReferencia;

        public PuntoController(IReferencia mReferencia)
        {
            this.mReferencia = mReferencia;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            try
            {
                return View(mReferencia.LisReferencia());
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            try
            {
                var puntoReferencia = mReferencia.FirstPunto(id);

                return View(puntoReferencia);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("PuntoReferenciaID,NombrePuntoReferencia,DireccionReferencia,Latitud,Longitud")] PuntoReferencia puntoReferencia)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(puntoReferencia);
                }

                mReferencia.SavePunto(puntoReferencia);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            try
            {
                var puntoReferencia = mReferencia.FindPunto(id);

                return View(puntoReferencia);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("PuntoReferenciaID,NombrePuntoReferencia,DireccionReferencia,Latitud,Longitud")] PuntoReferencia puntoReferencia)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(puntoReferencia);
                }

                mReferencia.UpdatePunto(puntoReferencia);
                return RedirectToAction(nameof(Index));
               
            }
            catch (Exception)
            {
                return NotFound();
            }
            
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try{
                var puntoReferencia = mReferencia.FirstPunto(id);

                mReferencia.RemovePunto(puntoReferencia);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }

        }
    }
}
