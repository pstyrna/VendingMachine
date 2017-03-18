using System.Collections.Generic;
using ChangeProvider;
using NSubstitute;
using NUnit.Framework;

namespace DOJO.Test
{
    [TestFixture]
    class ChangeProviderTest
    {
        private IChangeProvider _changeProvider;
        private ICoinsAvability _coinsAvability;

        [SetUp]
        public void SetUp()
        {
            _coinsAvability = Substitute.For<ICoinsAvability>();

            _changeProvider = new ChangeProvider(_coinsAvability);
        }

        [TestCase(0, 1, 0, 0, 0, new Coin[] {})]
        [TestCase(5,1,0,0,0, new[] { Coin.Nickel })]
        [TestCase(10,1,1,1,1, new[] { Coin.Dime })]
        [TestCase(10, 2, 0, 1, 1, new[] { Coin.Nickel, Coin.Nickel,  })]
        [TestCase(40,1,1,1,1, new[] { Coin.Quater, Coin.Dime, Coin.Nickel })]
        [TestCase(90,3,3,3,3, new[] { Coin.Quater, Coin.Quater, Coin.Quater, Coin.Dime, Coin.Nickel })]
        [TestCase(90, 3, 3, 2, 2, new[] { Coin.Quater, Coin.Quater, Coin.Dime, Coin.Dime, Coin.Dime, Coin.Nickel, Coin.Nickel })]
        [TestCase(115,1,1,1,1, new[] { Coin.Dollar, Coin.Dime, Coin.Nickel })]
        public void GetChange_ReturnCoins(int sum, int nickel, int dime, int quater, int dollar, IEnumerable<Coin> coins)
        {
            //Arrange
            _coinsAvability.CheckAvability(Coin.Nickel).Returns(nickel);
            _coinsAvability.CheckAvability(Coin.Dime).Returns(dime);
            _coinsAvability.CheckAvability(Coin.Quater).Returns(quater);
            _coinsAvability.CheckAvability(Coin.Dollar).Returns(dollar);
            //Act
            var result = _changeProvider.GetChange(sum);

            //Asset
            Assert.That(result, Is.EqualTo(coins));
        }

        [TestCase(5, 0, 0, 0, 0, new[] { Coin.Dollar, Coin.Dime, Coin.Nickel })]
        public void GetChange_ReturnNull_WhenNotEnoughtCoins(int sum, int nickel, int dime, int quater, int dollar, IEnumerable<Coin> coins)
        {
            //Arrange
            _coinsAvability.CheckAvability(Coin.Nickel).Returns(nickel);
            _coinsAvability.CheckAvability(Coin.Dime).Returns(dime);
            _coinsAvability.CheckAvability(Coin.Quater).Returns(quater);
            _coinsAvability.CheckAvability(Coin.Dollar).Returns(dollar);
            //Act
            var result = _changeProvider.GetChange(sum);

            //Asset
            Assert.That(result, Is.EqualTo(null));
        }
    }
}
