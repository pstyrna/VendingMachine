using DOJO;

namespace ChangeProvider
{
    public interface ICoinsAvability
    {
        int CheckAvability(Coin coin);
        void RemoveCoins(Coin coin, int howMany);
        void AddCoins(Coin coin, int howMany);
    }
}
