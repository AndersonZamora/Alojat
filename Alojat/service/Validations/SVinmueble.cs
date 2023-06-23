using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Alojat.service
{
    public class SVinmueble : IVinmueble
    {
        ModelStateDictionary modelState;

        private readonly IValidarCampos validarCampos;

        public SVinmueble(IValidarCampos validarCampos)
        {
            this.validarCampos = validarCampos;
        }

        public bool Validate(Inmueble inmueble, ModelStateDictionary modelState)
        {
            this.modelState = modelState;

            if (!ValididarCoordenadas($"{inmueble.LatitudInmueble},{inmueble.LongitudInmueble}")) return false;

            if (!ValididarDireccion(inmueble.DireccionInmueble)) return false;

            if (!ValidarCelular(inmueble.NumCelular)) return false;

            if (inmueble.PuntoReferenciaID == 0)
            {
                modelState.AddModelError("Punto", "Seleccione un punto");
                return false;
            }

            return true;
        }

        bool ValidarCelular(string NumCelular)
        {
            if (string.IsNullOrEmpty(NumCelular))
            {
                modelState.AddModelError("Celular", "El Celular es Obligatorio");
                return false;
            }
              
            if (!validarCampos.ValidarnUMEROS(NumCelular))
            {
                modelState.AddModelError("Celular", "Ingrese solo caracteres numericos");
                return false;
            }
             
            if (NumCelular.Length < 8 || NumCelular.Length > 9)
            {
                modelState.AddModelError("Celular", "Ingrese un numero de celular valido");
                return false;
            }

            return true;
        }

        bool ValididarDireccion(string direccionInmueble)
        {
            if (string.IsNullOrEmpty(direccionInmueble))
            {
                modelState.AddModelError("Direccion", "Este Campo es Obligatorio");
                return false;
            }
               
            if (!validarCampos.ValidarDireccion(direccionInmueble))
            {
                modelState.AddModelError("Direccion", "Ingrese una direccion correcta");
                return false;
            }

            return true;
        }

        bool ValididarCoordenadas(string coodenadas)
        {
            if (string.IsNullOrEmpty(coodenadas)) {

                modelState.AddModelError("Coordenadas", "Coordenadas no validas");
                return false;
            };

            if (!validarCampos.ValidarCoordenadas(coodenadas))
            {
                modelState.AddModelError("Coordenadas", "Coordenadas no validas");
                return false;
            }

            return true;
        }
    }
}
