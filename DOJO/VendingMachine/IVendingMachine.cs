using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOJO
{
    public interface IVendingMachine
    {
        Result Buy(IEnumerable<Coin> coins, Item item);
    }
}
