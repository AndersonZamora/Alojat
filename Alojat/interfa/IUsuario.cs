using Alojat.Models;

namespace Alojat.interfa
{
    public interface IUsuario
    {
        Usuario ValidarUsuario(string correo, string clave);
        void Logout(HttpContext context);
        void SaveUsuario(Usuario usuario);
        void SaveUsuarioRegistro(RegistroUsuario registro);
        IEnumerable<Usuario> ListUsuarios();
        Usuario FindUsuario(int id);
        Usuario FirstOr(string data);
        void UpdateUsuario(Usuario usuario);
        void UpdateUsuarioPass(UserUpdate usuario);
        void DeleteUsuario(Usuario usuario);
        Usuario GetUsuarioRol(string data);
    }
}
