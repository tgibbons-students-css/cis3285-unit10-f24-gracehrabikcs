using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class RestfulTradeDataProvider : ITradeDataProvider
    {
        string url;
        ILogger logger;
        HttpClient client = new HttpClient();

        public RestfulTradeDataProvider(string url, ILogger logger)
        {
            this.url = url;
            this.logger = logger;
        }

        async Task<List<string>> GetTradeAsync()
        {
            logger.LogInfo("Connecting to the Restful server using HTTP");
            List<string> tradesString = null;

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                // Read the content as a string and deserialize it into a List<string>
                string content = await response.Content.ReadAsStringAsync();
                tradesString = JsonSerializer.Deserialize<List<string>>(content);
                logger.LogInfo("Received trade strings of length = " + tradesString.Count);
            }
            return tradesString;
        }

        /*
        public IEnumerable<string> GetTradeData()
        {
            Task<List<string>> task = Task.Run(() => GetTradeAsync());
            task.Wait();

            List<string> tradeList = task.Result;
            return tradeList;
        }
        */

        // step 3
        public async Task<IEnumerable<string>> GetTradeData()
        {
            // Await the asynchronous GetTradeAsync method
            List<string> tradeList = await GetTradeAsync();

            // Return the result, if tradeList is null, return an empty list
            return tradeList ?? new List<string>();
        }
    }
}
