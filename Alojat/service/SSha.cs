using Alojat.interfa;
using Firebase.Auth.Providers;
using Firebase.Auth;
using Firebase.Storage;
using System.Security.Cryptography;
using System.Text;

namespace Alojat.service
{
    public class SSha : ISha
    {
        public string ConvertirSha256(string texto)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        public async Task<string> SubirStorage(Stream archivo, string nombre)
        {
            try
            {
                string email = "anderson@gmail.com";
                string clave = "ander123";
                string ruta = "Alojat";
                string api_key = "AIzaSyBAkb_MxELXgECQyJdj_ynVQxi7uBw_mPw";

                var config = new FirebaseAuthConfig
                {
                    ApiKey = api_key,
                    AuthDomain = "lobos-marinos.firebaseapp.com",
                    Providers = new FirebaseAuthProvider[]
                    {
                        new GoogleProvider().AddScopes(email),
                        new EmailProvider()
                    },
                };

                var auth = new FirebaseAuthClient(config);

                var client = await auth.SignInWithEmailAndPasswordAsync(email, clave);

                var user = client.User;
                var token = await user.GetIdTokenAsync();

                var task = new FirebaseStorage(
                    "lobos-marinos.appspot.com",
                     new FirebaseStorageOptions
                     {
                         AuthTokenAsyncFactory = () => Task.FromResult(token),
                         ThrowOnCancel = true,
                     })
                    .Child(ruta)
                    .Child(nombre)
                    .PutAsync(archivo);

                task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");
                var downloadUrl = await task;

                return downloadUrl;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
