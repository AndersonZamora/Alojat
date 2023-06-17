using Alojat.interfa;
using Alojat.Models;
using System.Security.Claims;

namespace Alojat.service
{
    public class SUserClaim : IUserClaim
    {

        public ClaimsIdent GetUser(IEnumerable<Claim> identity)
        {
            if (identity != null)
            {

                var name = identity.Where(c => c.Type == ClaimTypes.Name)
                   .Select(c => c.Value).SingleOrDefault();

                var email = identity.Where(c => c.Type == ClaimTypes.Email)
                  .Select(c => c.Value).SingleOrDefault();

                var role = identity.Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value).SingleOrDefault();

                ClaimsIdent user = new()
                {
                    Name = name,
                    Email = email,
                    Role = role
                };

                return user;
            }

            return new ClaimsIdent();
        }
    }
}
