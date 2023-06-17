using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Alojat.Controllers
{
    public class BuscarController : Controller
    {
        private readonly IBusquedad mBusquedad;
        private readonly IReferencia mReferencia;
        private readonly ICategoria mCategoria;
        public BuscarController(IBusquedad mBusquedad, IReferencia mReferencia, ICategoria mCategoria)
        {
            this.mBusquedad = mBusquedad;
            this.mReferencia = mReferencia;
            this.mCategoria = mCategoria;
        }

        public ViewResult Buscar()
        {
            ViewData["PuntoReferencia"] = new SelectList(mReferencia.LisReferencia(), "PuntoReferenciaID", "NombrePuntoReferencia");
            ViewData["Categoria"] = new SelectList(mCategoria.LisCategoria(), "CategoriaID", "NombreCategoria");

            return View(new List<CatPun>());
        }

        [HttpPost]
        public async Task<IActionResult> Buscar(Buscar buscar)
        {
            try
            {
                ViewData["PuntoReferencia"] = new SelectList(mReferencia.LisReferencia(), "PuntoReferenciaID", "NombrePuntoReferencia",buscar.Referencia);
                ViewData["Categoria"] = new SelectList(mCategoria.LisCategoria(), "CategoriaID", "NombreCategoria",buscar.Tipo);

                return View(await mBusquedad.CatPun(buscar));

            }
            catch (Exception)
            {
                return View(new List<CatPun>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var servicios = await mBusquedad.Servicios(id);

                if(servicios.Count() == 0)
                {
                    return RedirectToAction("Buscar");
                }

                return View(servicios);
            }
            catch (Exception)
            {
                return RedirectToAction("Buscar");
            }
        }
    }
}
