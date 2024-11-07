using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    internal class URLAsyncProvider : ITradeDataProvider
    {
        private readonly ITradeDataProvider baseProvider;
        public URLAsyncProvider(ITradeDataProvider baseProvider)
        {
            this.baseProvider = baseProvider;
        }
        public IEnumerable<string> GetTradeData()
        {
            Task<IEnumerable<string>> task = Task.Run(() => baseProvider.GetTradeData()); ;
            task.Wait();
            IEnumerable<string> lines = task.Result;
            return lines;
        }
    }
}
