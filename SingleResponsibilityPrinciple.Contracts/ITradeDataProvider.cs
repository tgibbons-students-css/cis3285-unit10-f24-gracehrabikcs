using System.Collections.Generic;

namespace SingleResponsibilityPrinciple.Contracts
{
    public interface ITradeDataProvider
    {
        // step 1
        Task<IEnumerable<string>> GetTradeData();

        //IEnumerable<string> GetTradeData();
    }
}