using Alojat.Data;
using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Alojat.service
{
    public class SVlogin : IVlogin
    {
        ModelStateDictionary modelState;

        private readonly IValidarCampos validarCampos;
        private readonly IUsuario mUsuario;
        private readonly AlquilerDbContext dbContext;

        public SVlogin(IValidarCampos validarCampos, IUsuario mUsuario, AlquilerDbContext dbContext)
        {
            this.validarCampos = validarCampos;
            this.mUsuario = mUsuario;
            this.dbContext = dbContext;

        }
        public bool Validate(LoginModel login, ModelStateDictionary modelState)
        {
            this.modelState = modelState;

            if (!ValididarCorreo(login.Email)) return false;
            if (!ValidarPasswor(login.Password)) return false;
            if (!ValidarCredenciales(login.Email, login.Password)) return false;

            return true;
        }

        bool ValididarCorreo(string correo)
        {
           
            if (string.IsNullOrEmpty(correo))
            {
                modelState.AddModelError("Correo", "Este Campo es Obligatorio");
                return false;
            }

            if (!validarCampos.ValidarEmail(correo))
            {
                modelState.AddModelError("Correo", "Ingrese una correo valido");
                return false;
            }

            return true;
        }

        bool ValidarPasswor(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                modelState.AddModelError("Contra", "Ingrese una contraseña");
                return false;
            }

            if (password.Contains(' '))
            {
                modelState.AddModelError("Contra", "Ingrese una contraseña sin espacios");
                return false;
            }

            return true;
        }

        bool ValidarCredenciales( string correo, string password)
        {
            var log = mUsuario.ValidarUsuario(correo, password);


            if (string.IsNullOrEmpty(log.Nombres))
            {
                modelState.AddModelError("Contra", "Credenciales incorrectas");
                return false;
            }

            return true;
        }

        public bool Register(RegistroUsuario registro, ModelStateDictionary modelState)
        {
            this.modelState = modelState;
   
            if (!ValidarNombre(registro.Nombres, "Nombre")) return false;

            if (!ValidarNombre(registro.Apellidos, "Apellido")) return false;

            if (!ValididarDireccion(registro.DireccionDomicilio)) return false;

            if (!ValidarCelular(registro.NumCelular)) return false;

            if (!ValidarFecha(registro.FechaNacimiento.ToString())) return false;

            if (!ValididarCorreoR(registro.Email)) return false;

            if (!ValidarPassworR(registro.Password)) return false;

            return true;
        }

        bool ValidarNombre(string nombres, string value)
        {
            if (string.IsNullOrEmpty(nombres))
            {
                modelState.AddModelError(value, $"El {value} es Obligatorio");

                return false;
            }

            if (!validarCampos.ValidarLetras(nombres))
            {
                modelState.AddModelError(value, "Solo ingrese caracteres alfabeticos");
                return false;
            }

            if (nombres.Length < 4)
            {
                modelState.AddModelError(value, "Ingrese un nombre válido");

                return false;
            }


            if (nombres.Length > 30)
            {
                modelState.AddModelError(value, "Ingrese un nombre válido");
                return false;
            }

            return true;
        }

        bool ValidarFecha(string fecha)
        {
            if (!validarCampos.ValidarFecha(fecha))
            {
                modelState.AddModelError("Fecha", "Seleccione un  fecha valida");
                return false;
            }

            return true;
        }

        bool ValidarCelular(string celular)
        {
            var usuario = dbContext.Usuario;

            if (string.IsNullOrEmpty(celular))
            {
                modelState.AddModelError("Celular", $"Este campo es Obligatorio");

                return false;
            }

            if (!validarCampos.ValidarnUMEROS(celular))
            {
                modelState.AddModelError("Celular", "Ingrese solo caracteres numericos");
                return false;
            }

            if (celular.Length < 8 || celular.Length > 9)
            {
                modelState.AddModelError("Celular", "Ingrese un numero de celular valido");
                return false;
            }

            if (usuario.Any(o => o.NumCelular == celular))
            {
                modelState.AddModelError("Celular", "Celular ya Registrado");
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

        bool ValidarPassworR(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                modelState.AddModelError("Contra", "Ingrese una contraseña");
                return false;
            }

            if (password.Contains(' '))
            {
                modelState.AddModelError("Contra", "Ingrese una contraseña sin espacios");
                return false;
            }

            if (password.Length < 6)
            {
                modelState.AddModelError("Contra", "Ingrese 6 caracteres como minimo");
                return false;
            }

            if (password.Length > 15)
            {
                modelState.AddModelError("Contra", "Maximo 15 caracteres");
                return false;
            }

            return true;
        }

        bool ValididarCorreoR(string correo)
        {
            var usuario = dbContext.Usuario;
            
            if (string.IsNullOrEmpty(correo))
            {
                modelState.AddModelError("Correo", "Este Campo es Obligatorio");
                return false;
            }

            if (!validarCampos.ValidarEmail(correo))
            {
                modelState.AddModelError("Correo", "Ingrese una correo valido");
                return false;
            }

            if (usuario.Any(o => o.Email == correo))
            {
                modelState.AddModelError("Correo", "Correo ya Registrado");
                return false;
            }

            return true;
        }
    }
}
