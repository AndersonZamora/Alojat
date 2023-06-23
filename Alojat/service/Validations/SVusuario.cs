using Alojat.Data;
using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Alojat.service
{
    public class SVusuario : IVusuario
    {
        ModelStateDictionary modelState;

        private readonly IValidarCampos validarCampos;
        private readonly AlquilerDbContext dbContext;

        public SVusuario(IValidarCampos validarCampos, AlquilerDbContext dbContext)
        {
            this.validarCampos = validarCampos;
            this.dbContext = dbContext;
        }

        public bool UpdateCate(Usuario usuario, ModelStateDictionary modelState)
        {
            this.modelState = modelState;

            var cate = dbContext.Usuario.Where(o => o.UsuarioID == usuario.UsuarioID);

            if (!cate.Any(o => o.NumCelular == usuario.NumCelular))
            {
                if (!ValidarCelular(usuario.NumCelular)) return false;

                return true;
            }

            if (!cate.Any(o => o.Email == usuario.Email))
            {
                if (!ValididarCorreo(usuario.Email)) return false;

                return true;
            }

            if (!ValidarNombre(usuario.Nombres, "Nombre")) return false;

            if (!ValidarNombre(usuario.Apellidos, "Apellido")) return false;

            if (!ValidarFecha(usuario.FechaNacimiento.ToString())) return false;

            if (!ValididarDireccion(usuario.DireccionDomicilio)) return false;

            if (usuario.RolID == 0)
            {
                modelState.AddModelError("Rol", "Seleccione un Rol");
                return false;
            }

            return true;
        }

        public bool Validate(Usuario usuario, ModelStateDictionary modelState)
        {
            this.modelState = modelState;

            if (!ValidarNombre(usuario.Nombres, "Nombre")) return false;

            if (!ValidarNombre(usuario.Apellidos, "Apellido")) return false;

            if (!ValidarFecha(usuario.FechaNacimiento.ToString())) return false;

            if (!ValidarCelular(usuario.NumCelular)) return false;

            if (!ValididarDireccion(usuario.DireccionDomicilio)) return false;

            if (!ValididarCorreo(usuario.Email)) return false;

            if (!ValidarPasswor(usuario.Password)) return false;

            if(usuario.RolID == 0)
            {
                modelState.AddModelError("Rol", "Seleccione un Rol");
                return false;
            }

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

        bool ValididarCorreo(string correo)
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

        public bool UpdatePass(UserUpdate usuario, ModelStateDictionary modelState)
        {
            this.modelState = modelState;

            try
            {
                var usuarioDB = dbContext.Usuario.Where(u => u.UsuarioID == usuario.UsuarioID).FirstOrDefault();

                if (usuarioDB == null)
                {
                    modelState.AddModelError("Contra", "Usuario invalido");
                    return false;
                }

               if (!ValidarPasswor(usuario.Password)) return false;

                return true;
            }
            catch (Exception)
            {
                modelState.AddModelError("Contra", "Usuario invalido");
                return false;
            }
        }
    }
}
