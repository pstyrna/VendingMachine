﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOJO
{
    public interface IChangeProvider
    {
	    bool TryGetChange(int value, out IEnumerable<Coin> change);
    }
}
