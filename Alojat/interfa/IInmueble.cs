using Alojat.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Alojat.interfa
{
    public interface IInmueble
    {
        List<Inmueble> ListInmueRefe();
        List<SelectListItem> SelectLis(int id);
        List<Inmueble> ListInmueRefeUsu(int id);
        void SaveInmueble(Inmueble inmueble);
        Inmueble FindInmu(int id);
        void RemoveInmueble(Inmueble inmueble);
        void UpdateInmueble(Inmueble inmueble);
    }
}
