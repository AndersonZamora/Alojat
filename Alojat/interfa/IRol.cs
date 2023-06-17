using Microsoft.AspNetCore.Mvc.Rendering;

namespace Alojat.interfa
{
    public interface IRol
    {
        IEnumerable<SelectListItem> ListRoles();
    }
}
