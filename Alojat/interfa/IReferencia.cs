using Alojat.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Alojat.interfa
{
    public interface IReferencia
    {
        List<PuntoReferencia> LisReferencia();
        PuntoReferencia FirstPunto(int id);
        void SavePunto(PuntoReferencia referencia);
        PuntoReferencia FindPunto(int id);
        void UpdatePunto(PuntoReferencia referencia);
        void RemovePunto(PuntoReferencia referencia);
        bool ExistsPunto(int id);
    }
}
