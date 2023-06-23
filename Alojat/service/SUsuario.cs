using Alojat.Data;
using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Alojat.service
{
    public class SUsuario : IUsuario
    {
        private readonly AlquilerDbContext dbContext;
        private readonly ISha sha;
        public SUsuario(AlquilerDbContext dbContext, ISha sha)
        {
            this.dbContext = dbContext;
            this.sha = sha;
        }

        public void DeleteUsuario(Usuario usuario)
        {
            dbContext.Usuario.Remove(usuario);
            dbContext.SaveChanges();
        }

        public Usuario FindUsuario(int id)
        {
            return dbContext.Usuario.Find(id);
        }

        public Usuario FirstOr(string data)
        {
            return dbContext.Usuario.Where(u => u.Email == data).FirstOrDefault();
        }

        public Usuario GetUsuarioRol(string data)
        {
            var usuario = dbContext.Usuario.Where(u => u.Email == data).Include(r => r.Rol).FirstOrDefault();

            return usuario;
        }

        public IEnumerable<Usuario> ListUsuarios()
        {
            IEnumerable<Usuario> lstUsuarios = dbContext.Usuario.Include(r => r.Rol);
            return lstUsuarios;
        }

        public void Logout(HttpContext context)
        {
            context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public void SaveUsuario(Usuario usuario)
        {
            usuario.Password = sha.ConvertirSha256(usuario.Password);
            usuario.FechaRegistro = DateTime.Now;
            dbContext.Usuario.Add(usuario);
            dbContext.SaveChanges();
        }

        public void SaveUsuarioRegistro(RegistroUsuario registro)
        {
            Usuario usuario = new()
            {
                Nombres = registro.Nombres,
                Apellidos = registro.Apellidos,
                FechaNacimiento = registro.FechaNacimiento,
                NumCelular = registro.NumCelular,
                DireccionDomicilio = registro.DireccionDomicilio,
                Email = registro.Email,
                Password = sha.ConvertirSha256(registro.Password),
                RolID = 1,
                FechaRegistro = DateTime.Now
            };

            dbContext.Usuario.Add(usuario);
            dbContext.SaveChanges();
        }

        public void UpdateUsuario(Usuario usuario)
        {
            var usuarioDB = dbContext.Usuario.Where(u => u.UsuarioID == usuario.UsuarioID).FirstOrDefault();

            usuarioDB.Nombres = usuario.Nombres;
            usuarioDB.Apellidos = usuario.Apellidos;
            usuarioDB.FechaNacimiento = usuario.FechaNacimiento;
            usuarioDB.NumCelular = usuario.NumCelular;
            usuarioDB.DireccionDomicilio = usuario.DireccionDomicilio;
            usuarioDB.Email = usuario.Email;
            usuarioDB.RolID = usuario.RolID;

            dbContext.SaveChanges();
        }

        public void UpdateUsuarioPass(UserUpdate usuario)
        {
            var usuarioDB = dbContext.Usuario.Where(u => u.UsuarioID == usuario.UsuarioID).FirstOrDefault();

            var newPass = usuario.Password = sha.ConvertirSha256(usuario.Password);

            usuarioDB.Password = newPass;

            dbContext.SaveChanges();
        }

        public Usuario ValidarUsuario(string correo, string clave)
        {
            try
            {
                clave = sha.ConvertirSha256(clave);

                var usuario = dbContext.Usuario.Include(r => r.Rol)
                                                .Where(u => u.Email == correo)
                                                .Where(u => u.Password == clave)
                                                .FirstOrDefault();

                if (usuario == null) return new();

                return usuario;
            }
            catch (Exception)
            {
                return new();
            }
        }
    }
}
