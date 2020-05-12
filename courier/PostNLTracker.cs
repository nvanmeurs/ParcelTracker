using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParcelTracker.courier
{
    class PostNLTracker : ICourierTracker
    {
        private const string API_URL = "https://www.internationalparceltracking.com/api";

        // TODO: Define ParcelInformation object
        public async Task<object> GetParcelStatus(ParcelDefinition parcelDefinition)
        {
            var barcode = parcelDefinition.Barcode;
            var postalCode = parcelDefinition.PostalCode;
            var country = parcelDefinition.DestinationCountry;
            var apiUrl = $"{API_URL}/shipment?barcode={barcode}&country={country}&language=en&postalCode={postalCode}";

            var httpClient = new HttpClient();
            var apiResponse = await httpClient.GetStringAsync(apiUrl);

            // TODO: Parse JSON into ParcelInformation object
            var parcelStatus = JObject.Parse(apiResponse);
            return parcelStatus;
        }
    }
}
