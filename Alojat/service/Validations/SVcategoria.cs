using Alojat.Data;
using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Alojat.service
{
    public class SVcategoria : IVcategoria
    {
        ModelStateDictionary modelState;

        private readonly IValidarCampos validarCampos;
        private readonly AlquilerDbContext dbContext;

        public SVcategoria(IValidarCampos validarCampos, AlquilerDbContext dbContext)
        {
            this.validarCampos = validarCampos;
            this.dbContext = dbContext;
        }

        public bool Validate(Categoria categoria, ModelStateDictionary modelState)
        {
            this.modelState = modelState;

            if (!ValidarCateg(categoria.NombreCategoria)) return false;

            return true;

        }

        bool ValidarCateg(string categoria)
        {
            var categ = dbContext.Categoria;

            if (string.IsNullOrEmpty(categoria))
            {
                modelState.AddModelError("Categ", "Este campo es requerido");
                return false;
            }

            if (!validarCampos.ValidarLetrasNumeros(categoria))
            {
                modelState.AddModelError("Categ", "Solo ingrese letras y numeros");
                return false;
            }

            if (categ.Any(o => o.NombreCategoria == categoria))
            {
                modelState.AddModelError("Categ", "Ya Exite Esta Categoria");
                return false;
            }

            return true;
        }

        public bool UpdateCate(Categoria categoria, ModelStateDictionary modelState)
        {

            this.modelState = modelState;

            var cate = dbContext.Categoria.Where(o => o.CategoriaID == categoria.CategoriaID);
            
            if (!cate.Any(o => o.NombreCategoria == categoria.NombreCategoria))
            {
                if (!ValidarCateg(categoria.NombreCategoria)) return false;

                return true;
            }

            return true;
        }
    }
}
