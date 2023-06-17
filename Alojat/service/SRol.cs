using Alojat.Data;
using Alojat.interfa;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Alojat.service
{
    public class SRol : IRol
    {
        private readonly AlquilerDbContext dbContext;
        public SRol(AlquilerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<SelectListItem> ListRoles()
        {
            return dbContext.Rol.Select(r => new SelectListItem
            {
                Text = r.DescripcionRol,
                Value = r.RolID.ToString()
            });
        }
    }
}
