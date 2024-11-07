using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class AdjustTradeDataProvider : ITradeDataProvider
    {
        private readonly ITradeDataProvider origProvider;

        public AdjustTradeDataProvider(ITradeDataProvider origProvider)
        {
            this.origProvider = origProvider;
        }

        /*
        public IEnumerable<string> GetTradeData()
        {
            // Call the original provider's GetTradeData()
            IEnumerable<string> tradeData = origProvider.GetTradeData();

            // Replace "GBP" with "EUR" in each line of text
            return tradeData.Select(line => line.Replace("GBP", "EUR"));
        }
        */

        // step 5 cont.
        // This method should now return an IAsyncEnumerable<string>
        public async IAsyncEnumerable<string> GetTradeData()
        {
            await foreach (var line in origProvider.GetTradeData())
            {
                // Optionally, you can adjust the trade data here before yielding it
                yield return line;
            }
        }
    }
}
