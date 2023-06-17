
using Alojat.Models;
using System.Security.Claims;

namespace Alojat.interfa
{
    public interface IUserClaim
    {
        ClaimsIdent GetUser(IEnumerable<Claim> identity);
    }
}
