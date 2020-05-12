using System;
using System.Threading.Tasks;

namespace ParcelTracker.courier
{
    class DHLTracker : ICourierTracker
    {
        public Task<object> GetParcelStatus(ParcelDefinition parcelDefinition)
        {
            throw new NotImplementedException();
        }
    }
}
