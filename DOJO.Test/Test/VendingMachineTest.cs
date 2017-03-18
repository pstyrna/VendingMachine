using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;

namespace DOJO.Test
{
    [TestFixture]
    class VendingMachineTest
    {
        private IVendingMachine _vendingMachine;
        private IChangeProvider _changeProvider;

        [SetUp]
        public void SetUp()
        {
            _changeProvider = Substitute.For<IChangeProvider>();

            _vendingMachine = new VendingMachine(_changeProvider);
        }

        [TestCase(Item.A)]
        [TestCase(Item.B)]
        [TestCase(Item.C)]
        public void Buy_ReturSelectedItem(Item selectedItem)
        {
            //Arrange

            //Act
            var result = _vendingMachine.Buy(new Coin[] { Coin.Dollar, Coin.Dollar}, selectedItem);

            //Asset
            Assert.That(result.Item, Is.EqualTo(selectedItem));
        }

        [TestCase(Item.A)]
        [TestCase(Item.B)]
        [TestCase(Item.C)]
        public void Buy_ReturnsNull_WhenNotEnoughCoins(Item selectedItem)
        {
            //Arrange
            var coins = new Coin[] {};

            //Act
            var result = _vendingMachine.Buy(coins, selectedItem);

            //Asset
            Assert.That(result.Item, Is.EqualTo(null));
        }

        [Test]
        public void Buy_ReturnsBackCoins_WhenNotEnoughCoins()
        {
            //Arrange
            var coins = new Coin[] { Coin.Nickel, Coin.Dime,  };

            //Act
            var result = _vendingMachine.Buy(coins, Item.A);

            //Asset
            Assert.That(result.Item, Is.EqualTo(null));
            Assert.That(result.Change.Select(c => (int)c).Sum(), Is.EqualTo(15));
        }

        [Test]
        public void Buy_ReturnsChangeFromChangeProviderWithItem()
        {
            //Arrange
            var coins = new Coin[] { Coin.Dollar };
            var change = new Coin[] {Coin.Quater, Coin.Dime};

            _changeProvider.GetChange(35).Returns(change);

            //Act
            var result = _vendingMachine.Buy(coins, Item.A);

            //Asset
            Assert.That(result.Item, Is.EqualTo(Item.A));
            Assert.That(result.Change, Is.EqualTo(change));
        }

        [Test]
        public void Buy_ReturnBackCoins_WhenNoCoinsForChange()
        {
            //Arrange
            var coins = new Coin[] { Coin.Dollar };
            var change = new Coin[] { Coin.Quater, Coin.Dime };

            _changeProvider.GetChange(35).Returns((IEnumerable<Coin>) null);

            //Act
            var result = _vendingMachine.Buy(coins, Item.A);

            //Asset
            Assert.That(result.Item, Is.EqualTo(null));
            Assert.That(result.Change, Is.EqualTo(coins));
        }
    }
}
