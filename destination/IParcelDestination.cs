using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParcelTracker.destination
{
    interface IParcelDestination
    {
        // TODO: Define ParcelInformation object
        Task WriteParcels(List<object> parcels);
    }
}
