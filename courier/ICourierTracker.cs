using System.Threading.Tasks;

namespace ParcelTracker.courier
{
    interface ICourierTracker
    {
        // TODO: Define ParcelInformation object
        Task<object> GetParcelStatus(ParcelDefinition parcelDefinition);
    }
}
