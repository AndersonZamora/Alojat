using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alojat.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoria mCategoria;
        private readonly IVcategoria mVcategoria;

        public CategoriaController(ICategoria mCategoria, IVcategoria mVcategoria)
        {
            this.mCategoria = mCategoria;
            this.mVcategoria = mVcategoria;
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
            if (!mVcategoria.Validate(categoria,ModelState))
            {
                return View(categoria);
            }

            mCategoria.SaveCategoria(categoria);
            return RedirectToAction(nameof(Index));

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

            if (!mVcategoria.UpdateCate(categoria, ModelState))
            {
                return View("Edit", categoria);
            }

            mCategoria.UpdateCategoria(categoria);
            return RedirectToAction(nameof(Index));
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
