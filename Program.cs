using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TrelloParcelTracker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter country, e.g. NL");
            var country = Console.ReadLine();

            Console.WriteLine("Enter postal code, e.g. 1234AB");
            var postalCode = Console.ReadLine();

            Console.WriteLine("Enter parcel barcode, e.g. 3SABCD428609");
            var barcode = Console.ReadLine();

            dynamic parcelStatus = await GetParcelStatus(country, postalCode, barcode);
            Console.WriteLine($"Shipment status: {parcelStatus.shipmentPhase}");
        }

        private static async Task<object> GetParcelStatus(string country, string postalCode, string barcode)
        {
            var httpClient = new HttpClient();
            var apiUrl = $"https://www.internationalparceltracking.com/api/shipment?barcode={barcode}&country={country}&language=en&postalCode={postalCode}";
            var apiResponse = await httpClient.GetStringAsync(apiUrl);
            var parcelStatus = JsonConvert.DeserializeObject(apiResponse);
            return parcelStatus;
        }
    }
}
