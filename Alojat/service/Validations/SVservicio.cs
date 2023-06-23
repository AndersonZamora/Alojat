using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Alojat.service.Validations
{
    public class SVservicio : IVservicio
    {
        ModelStateDictionary modelState;

        private readonly IValidarCampos validarCampos;
        public SVservicio(IValidarCampos validarCampos)
        {
            this.validarCampos = validarCampos;
        }

        public bool Validate(Servicio servicio, ModelStateDictionary modelState)
        {
            this.modelState = modelState;

            if (!ValidarPiso(servicio.UbicacionPiso)) return false;

            if (!ValidarDescrip(servicio.DescripcionServicio)) return false;

            if (!ValidarTipo(servicio.TipoServicio)) return false;

            if (!ValidarPrecio(servicio.Precio.ToString())) return false;

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

        bool ValidarPiso(string piso)
        {
            if (string.IsNullOrEmpty(piso))
            {
                modelState.AddModelError("Piso", "Este campo es requerido");
                return false;
            }

            if (!validarCampos.ValidarLetrasNumeros(piso))
            {
                modelState.AddModelError("Piso", "Solo ingrese letras y numeros");
                return false;
            }

            return true;
        }

        bool ValidarPrecio(string precio)
        {
            if (string.IsNullOrEmpty(precio))
            {
                modelState.AddModelError("Precio", "Este campo es requerido");
                return false;
            }

            if (!validarCampos.Precio(precio))
            {
                modelState.AddModelError("Precio", "Ingrese un precio valido");
                return false;
            }

            return true;
        }

        bool ValidarTipo(string tipo)
        {
            if (string.IsNullOrEmpty(tipo))
            {
                modelState.AddModelError("Tipo", "Este campo es requerido");
                return false;
            }

            if (string.IsNullOrWhiteSpace(tipo))
            {
                modelState.AddModelError("Tipo", "Solo ingrese letras");
                return false;
            }

            if (!validarCampos.ValidarSoloLetras(tipo))
            {
                modelState.AddModelError("Tipo", "Ingrese solo letras");
                return false;
            }

            return true;
        }

        bool ValidarDescrip(string descrip)
        {
            if (string.IsNullOrEmpty(descrip))
            {
                modelState.AddModelError("Descrip", "Este campo es requerido");
                return false;
            }

            if (!validarCampos.ValidarLetrasNumeros(descrip))
            {
                modelState.AddModelError("Descrip", "Solo ingrese letras y numeros");
                return false;
            }

            return true;
        }
    }
}
