using Alojat.Data;
using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Alojat.service
{
    public class SVinmueble : IVinmueble
    {
        private ModelStateDictionary modelState;

        public bool Validate(Inmueble inmueble, ModelStateDictionary modelState)
        {
            this.modelState = modelState;

            if (!ValidarImagen(inmueble.NumCelular))
            {
                modelState.AddModelError("Celular", "El Celular requerido");
                return false;
            }

            if (!ValidarImagen(inmueble.DireccionInmueble))
            {
                modelState.AddModelError("Dirección", "La dirección requerida");
                return false;
            }

            if (!ValidarImagen(inmueble.LatitudInmueble))
            {
                modelState.AddModelError("Latitud", "Latitud requerida");
                return false;
            }

            if (!ValidarImagen(inmueble.LongitudInmueble))
            {
                modelState.AddModelError("Longitud", "Longitud requerida");
                return false;
            }

            if (inmueble.PuntoReferenciaID == 0)
            {
                modelState.AddModelError("Punto", "Seleccione un punto");
                return false;
            }

            return true;
        }

        public void ValidateUpdate(Inmueble inmueble)
        {
            throw new NotImplementedException();
        }

        bool ValidarImagen(string inmueble)
        {
            return (!string.IsNullOrEmpty(inmueble));
        }

        bool ValidarId(int id)
        {
            return (id == 0);
        }
    }
}
