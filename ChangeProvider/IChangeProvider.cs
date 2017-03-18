using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOJO
{
    public interface IChangeProvider
    {
        IEnumerable<Coin> GetChange(int value);
    }
}
