using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Alojat.service.Validations
{
    public class SVservicio : IVservicio
    {
        private ModelStateDictionary modelState;

        public bool Validate(Servicio servicio, ModelStateDictionary modelState)
        {
            this.modelState = modelState;

            if (!ValidarImagen(servicio.UbicacionPiso))
            {
                modelState.AddModelError("Ubicacion", "Ubicación requerida");
                return false;
            }

            if (!ValidarImagen(servicio.DescripcionServicio))
            {
                modelState.AddModelError("Descripcion", "Descripción requerida");
                return false;
            }

            if (!ValidarImagen(servicio.TipoServicio))
            {
                modelState.AddModelError("Tipo", "Tipo de servicio");
                return false;
            }

            if (servicio.Precio == 0)
            {
                modelState.AddModelError("Precio", "Precio requerido");
                return false;
            }

            if (servicio.CategoriaID == 0)
            {
                modelState.AddModelError("Categoria", "Seleccione categoria");
                return false;
            }

            if (servicio.InmuebleID == 0)
            {
                modelState.AddModelError("Inmueble", "Seleccione categoria");
                return false;
            }

            return true;
        }

        public void ValidateUpdate(Servicio servicio)
        {
            throw new NotImplementedException();
        }

        private static bool ValidarImagen(string servicio)
        {
            return (!string.IsNullOrEmpty(servicio));
        }
    }
}
