using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Headers;

namespace airtimegiveaway
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();

        static string  BASE_API_URL = "https://api.flutterwave.com/v3/bills";
        static string SEC_KEY = "Your Secret Key";
        static async Task Main(string[] args)
        {
            Guid reference = Guid.NewGuid();
            Console.WriteLine(reference);
            try
            {
                var payload = new
                {

                    country = "NG",
                   customer = "Your phone number",
                        amount = 100,
                        recurrence = "ONCE",
                        type = "AIRTIME",
                        reference = reference
                };
               

                string json = JsonSerializer.Serialize(payload);
                HttpContent content = new StringContent(json);
                content.Headers.ContentType.MediaType = "application/json";
                client.DefaultRequestHeaders.Authorization=  new AuthenticationHeaderValue("Bearer", SEC_KEY);
                HttpResponseMessage response = await client.PostAsync(BASE_API_URL, content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
               }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
   
    }
}
