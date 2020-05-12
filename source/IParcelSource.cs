using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParcelTracker.source
{
    interface IParcelSource
    {
        Task<List<ParcelDefinition>> GetParcels();
    }
}
