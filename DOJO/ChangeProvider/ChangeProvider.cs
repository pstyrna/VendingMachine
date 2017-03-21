using System.Collections.Generic;
using System.Collections.ObjectModel;
using ChangeProvider;

namespace DOJO
{
    public class ChangeProvider : IChangeProvider
    {
        private readonly ICoinsAvability _coinsAvability;

        public ChangeProvider(ICoinsAvability coinsAvability)
        {
            _coinsAvability = coinsAvability;
        }

		public bool TryGetChange(int value, out IEnumerable<Coin> change)
		{
			change = new Collection<Coin>();

            int dollarGiven = value / (int) Coin.Dollar;
            if (dollarGiven != 0)
            {
                int dollarAvaliable = _coinsAvability.CheckAvability(Coin.Dollar);
                if (dollarAvaliable < dollarGiven)
                {
                    dollarGiven = dollarAvaliable;
                }
                value = value - dollarGiven * (int)Coin.Dollar;
            }
            
            int quaterGiven = value / (int) Coin.Quater;
            if (quaterGiven != 0)
            {
                int quaterAvaliable = _coinsAvability.CheckAvability(Coin.Quater);
                if (quaterAvaliable < quaterGiven)
                {
                    quaterGiven = quaterAvaliable;
                }
                value = value - quaterGiven * (int) Coin.Quater;
            }

            int dimeGiven = value / (int) Coin.Dime;
            if (dimeGiven != 0)
            {
                int dimeAvaliable = _coinsAvability.CheckAvability(Coin.Dime);
                if (dimeAvaliable < dimeGiven)
                {
                    dimeGiven = dimeAvaliable;
                }
                value = value - dimeGiven * (int) Coin.Dime;
            }

            int nickelGiven = value / (int) Coin.Nickel;
            if (nickelGiven != 0)
            {
                int nickelAvaliable = _coinsAvability.CheckAvability(Coin.Nickel);
                if (nickelAvaliable < nickelGiven)
                {
                    nickelGiven = nickelAvaliable;
                }
                value = value - nickelGiven * (int) Coin.Nickel;
            }

            if (value != 0) 
            {
                return false; //not enough coins for change
            }

            if (dollarGiven != 0)
            {
                _coinsAvability.RemoveCoins(Coin.Dollar, dollarGiven);
                for (var i = 0; i < dollarGiven; i++)
                {
                    change.Add(Coin.Dollar); //jak to zrobic ? 
                }
            }
            if (quaterGiven != 0)
            {
                _coinsAvability.RemoveCoins(Coin.Quater, quaterGiven);
                for (var i = 0; i < quaterGiven; i++)
                {
                    change.Add(Coin.Quater);
                }
            }
            if (dimeGiven != 0)
            {
                _coinsAvability.RemoveCoins(Coin.Dime, dimeGiven);
                for (var i = 0; i < dimeGiven; i++)
                {
                    change.Add(Coin.Dime);
                }
            }
            if (nickelGiven != 0)
            {
                _coinsAvability.RemoveCoins(Coin.Nickel, nickelGiven);
                for (var i = 0; i < nickelGiven; i++)
                {
                    change.Add(Coin.Nickel);
                }
            }
            return true;
        }
    }
}