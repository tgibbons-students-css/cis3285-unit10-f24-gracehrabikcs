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


        public IEnumerable<string> GetTradeData()
        {
            // Call the original provider's GetTradeData()
            IEnumerable<string> tradeData = origProvider.GetTradeData();

            // Replace "GBP" with "EUR" in each line of text
            return tradeData.Select(line => line.Replace("GBP", "EUR"));
        }
    }
}
