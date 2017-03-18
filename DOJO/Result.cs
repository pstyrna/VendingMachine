using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOJO
{
    public class Result
    {
        public Item? Item { get; set; }
        public IEnumerable<Coin> Change { get; set; }
    }
}
