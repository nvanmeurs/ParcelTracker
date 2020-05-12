using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParcelTracker.source
{
    class TrelloParcelSource : IParcelSource
    {
        public Task<List<ParcelDefinition>> GetParcels()
        {
            throw new NotImplementedException();
        }
    }
}
