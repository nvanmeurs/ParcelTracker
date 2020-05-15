using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParcelTracker.source
{
    class JsonParcelSource : IParcelSource
    {
        public Task<List<ParcelDefinition>> GetParcels()
        {
            throw new NotImplementedException();
        }
    }
}
