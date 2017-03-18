using System.Collections.Generic;
using System.Linq;

namespace DOJO
{
    public class VendingMachine : IVendingMachine
    {
        private readonly IChangeProvider _changeProvider;

        public VendingMachine(IChangeProvider changeProvider)
        {
            _changeProvider = changeProvider;
        }

        public Result Buy(IEnumerable<Coin> coins, Item item)
        {
            var result = new Result();
            var sum = coins.Select(c => (int)c).Sum();
            if (sum >= (int) item)
            {
                result.Change = _changeProvider.GetChange(sum - (int) item);
                if (result.Change == null)
                {
                    result.Change = coins;
                    result.Item = null;
                }
                else
                {
                    result.Item = item;
                }
            }
            else
            {
                result.Change = coins;
            }
            return result;
        }
    }
}
