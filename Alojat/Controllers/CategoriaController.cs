using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alojat.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoria mCategoria;

        public CategoriaController(ICategoria mCategoria)
        {
            this.mCategoria = mCategoria;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(mCategoria.LisCategoria());
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CategoriaID,NombreCategoria")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                mCategoria.SaveCategoria(categoria);
                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            if (!mCategoria.ValidateCategoria(id))
            {
                return NotFound();
            }

            var categoria = mCategoria.FindCategoria(id);

            if (categoria == null)
            {
                return NotFound();

            }

            return View(categoria);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CategoriaID,NombreCategoria")] Categoria categoria)
        {
            if (id != categoria.CategoriaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                mCategoria.UpdateCategoria(categoria);
                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            if (!mCategoria.ValidateCategoria(id))
            {
                return NotFound();
            }

            var categoria = mCategoria.FirstCategoria(id);

            mCategoria.RemoveCategoria(categoria);

            return RedirectToAction(nameof(Index));
        }
    }
}
