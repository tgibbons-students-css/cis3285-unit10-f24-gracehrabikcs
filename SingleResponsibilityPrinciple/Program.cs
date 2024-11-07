using System;
using System.Reflection;

using SingleResponsibilityPrinciple.AdoNet;
using SingleResponsibilityPrinciple.Contracts;

namespace SingleResponsibilityPrinciple
{
    class Program
    {

        // step 5 cont.
        static async Task Main(string[] args)
        {
            ILogger logger = new ConsoleLogger();
            // Open up the local textfile as a stream
            string fileName = "SingleResponsibilityPrinciple.trades.txt";
            Stream tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName);

            if (tradeStream == null)
            {
                logger.LogWarning("trade file could not be opened at " + fileName);
                Environment.Exit(1); // Exits the application with a non-zero exit code indicating an error
            }

            // URL to read trade file from
            string tradeURL = "http://raw.githubusercontent.com/tgibbons-css/CIS3285_Unit9_F24/refs/heads/master/SingleResponsibilityPrinciple/trades.txt";
            string restfulURL = "http://unit9trader.azurewebsites.net/api/TradeData";

            ITradeValidator tradeValidator = new SimpleTradeValidator(logger);

            // These are three different trade providers that read from different sources
            ITradeDataProvider fileProvider = new StreamTradeDataProvider(tradeStream, logger);
            ITradeDataProvider urlProvider = new URLTradeDataProvider(tradeURL, logger);

            ITradeMapper tradeMapper = new SimpleTradeMapper();
            ITradeParser tradeParser = new SimpleTradeParser(tradeValidator, tradeMapper);
            ITradeStorage tradeStorage = new AdoNetTradeStorage(logger);

            TradeProcessor tradeProcessor = new TradeProcessor(urlProvider, tradeParser, tradeStorage);

            // Step 5 - Asynchronous method call
            await tradeProcessor.ProcessTradesAsync();

            // Testing part 1 - Adjusted Provider Example
            ITradeDataProvider origProvider = new URLTradeDataProvider(tradeURL, logger);
            ITradeDataProvider adjustProvider = new AdjustTradeDataProvider(origProvider);

            // Await asynchronous GetTradeData (if updated to return Task)
            await foreach (var line in adjustProvider.GetTradeData())
            {
                Console.WriteLine(line);
            }

            // Testing part 2 - Asynchronous URLProvider
            ITradeDataProvider baseProvider = new URLTradeDataProvider(tradeURL, logger); // Assuming this is an asynchronous provider now
            URLAsyncProvider asyncProvider = new URLAsyncProvider(baseProvider);

            // Await asynchronous GetTradeData (if updated to return Task)
            var tradeData = await asyncProvider.GetTradeData();

            foreach (var line in tradeData)
            {
                Console.WriteLine(line);
            }

            // Optionally, you could keep this for testing purposes
            // Console.ReadKey();
        }



        /*
        static async Task Main(string[] args)
        {
            ILogger logger = new ConsoleLogger();
            // Open up the local textfile as a stream
            String fileName = "SingleResponsibilityPrinciple.trades.txt";
            Stream tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName);
            if (tradeStream == null)
            {
                logger.LogWarning("trade file could not be openned at " + fileName);
                Environment.Exit(1); // Exits the application with a non-zero exit code indicating an error
            }
            // data file to read from locally
            //Stream tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Unit9_Trader.trades.txt");
            
            // URL to read trade file from
            string tradeURL = "http://raw.githubusercontent.com/tgibbons-css/CIS3285_Unit9_F24/refs/heads/master/SingleResponsibilityPrinciple/trades.txt";
            //Two different URLs for Restful API
            //string restfulURL = "http://localhost:22359/api/TradeData";
            string restfulURL = "http://unit9trader.azurewebsites.net/api/TradeData";

            ITradeValidator tradeValidator = new SimpleTradeValidator(logger);

            //These are three different trade providers that read from different sources
            ITradeDataProvider fileProvider = new StreamTradeDataProvider(tradeStream, logger);
            ITradeDataProvider urlProvider = new URLTradeDataProvider(tradeURL, logger);
            //ITradeDataProvider restfulProvider = new RestfulTradeDataProvider(restfulURL, logger);

            ITradeMapper tradeMapper = new SimpleTradeMapper();
            ITradeParser tradeParser = new SimpleTradeParser(tradeValidator, tradeMapper);
            ITradeStorage tradeStorage = new AdoNetTradeStorage(logger);

            TradeProcessor tradeProcessor = new TradeProcessor(urlProvider, tradeParser, tradeStorage);
            //TradeProcessor tradeProcessor = new TradeProcessor(urlProvider, tradeParser, tradeStorage);

            tradeProcessor.ProcessTrades();
   
          

            // Testing part 1
            ITradeDataProvider origProvider = new URLTradeDataProvider(tradeURL, logger); // assuming this is defined and returns IEnumerable<string>
            ITradeDataProvider adjustProvider = new AdjustTradeDataProvider(origProvider);

            foreach (var line in adjustProvider.GetTradeData())
            {
                Console.WriteLine(line);
            }

            // Testing part 2
            ITradeDataProvider baseProvider = new URLTradeDataProvider(tradeURL, logger); // Assuming this is a synchronous provider
            URLAsyncProvider asyncProvider = new URLAsyncProvider(baseProvider);

            // Fetch the trade data asynchronously
            var tradeData = asyncProvider.GetTradeData();

            foreach (var line in tradeData)
            {
                Console.WriteLine(line);
            }

            //Console.ReadKey();
        */


    }
        
    }
}
