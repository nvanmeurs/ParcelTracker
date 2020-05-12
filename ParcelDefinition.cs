using ParcelTracker.courier;

namespace ParcelTracker
{
    class ParcelDefinition
    {
        public string Description { get; set; }

        public string Barcode { get; set; }

        public CourierImplementation Courier { get; set; }

        public string PostalCode { get; set; }

        public string DestinationCountry { get; set; }
    }
}
