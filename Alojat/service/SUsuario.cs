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

        public void SaveUsuarioRegistro(Usuario usuario)
        {
            usuario.Password = sha.ConvertirSha256(usuario.Password);
            usuario.RolID = 3;
            usuario.FechaRegistro = DateTime.Now;
            dbContext.Usuario.Add(usuario);
            dbContext.SaveChanges();
        }

        public void UpdateUsuario(Usuario usuario)
        {
            var query = from u in dbContext.Usuario where u.UsuarioID == usuario.UsuarioID select u.Password;
            usuario.Password = query.ToString();

            dbContext.Usuario.Update(usuario);
            dbContext.SaveChanges();
        }

        public Usuario ValidarUsuario(string correo, string clave)
        {
            clave = sha.ConvertirSha256(clave);

            var usuario = dbContext.Usuario.Include(r => r.Rol)
                                            .Where(u => u.Email == correo)
                                            .Where(u => u.Password == clave)
                                            .FirstOrDefault();
            return usuario;
        }
    }
}
