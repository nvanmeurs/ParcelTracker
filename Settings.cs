using ParcelTracker.destination;
using ParcelTracker.source;

namespace ParcelTracker
{
    class Settings
    {
        public ParcelSourceImplementation ParcelSource { get; set; }
        public ParcelDestinationImplementation ParcelDestination { get; set; }

        public string TrelloBoardId { get; set; }
        public string TrelloAppKey { get; set; }
        public string TrelloUserToken { get; set; }

        public string DefaultDestinationCountry { get; set; }
        public string DefaultPostalCode { get; set; }
    }
}
