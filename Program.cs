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

        static string BASE_API_URL = "https://api.flutterwave.com/v3/bills";
        static string SEC_KEY = "secret-key";
        static string[] PHONE_NO = { };
        static int AMOUNT = 200;

        static async Task Main(string[] args)
        {
            foreach (var num in PHONE_NO)
            {
                Guid reference = Guid.NewGuid();
                try
                {
                    var payload = new
                    {

                        country = "NG",
                        customer = num,
                        amount = AMOUNT,
                        recurrence = "ONCE",
                        type = "AIRTIME",
                        reference = reference
                    };


                    string serializedPayload = JsonSerializer.Serialize(payload);
                    HttpContent content = new StringContent(serializedPayload);
                    content.Headers.ContentType.MediaType = "application/json";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SEC_KEY);
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
}
