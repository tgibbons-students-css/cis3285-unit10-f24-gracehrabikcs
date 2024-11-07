
using SingleResponsibilityPrinciple.Contracts;

namespace SingleResponsibilityPrinciple
{
    public class TradeProcessor
    {
        public TradeProcessor(ITradeDataProvider tradeDataProvider, ITradeParser tradeParser, ITradeStorage tradeStorage)
        {
            this.tradeDataProvider = tradeDataProvider;
            this.tradeParser = tradeParser;
            this.tradeStorage = tradeStorage;
        }


        /*
        public void ProcessTrades()
        {
            var lines = tradeDataProvider.GetTradeData();
            var trades = tradeParser.Parse(lines);
            tradeStorage.Persist(trades);
        }
        */

        // step 5
        // Make ProcessTrades async since GetTradeData() now returns Task<IEnumerable<string>>
        public async Task ProcessTradesAsync()
        {
            // Use await to get trade data asynchronously
            var lines = await tradeDataProvider.GetTradeData();

            // Process the trade data (parse and store)
            var trades = tradeParser.Parse(lines);
            tradeStorage.Persist(trades);
        }


        private readonly ITradeDataProvider tradeDataProvider;
        private readonly ITradeParser tradeParser;
        private readonly ITradeStorage tradeStorage;
    }
}
