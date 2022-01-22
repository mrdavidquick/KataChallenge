using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Checkout.Services.Test
{
    public class CheckoutTests
    {
        [Fact]
        public void Checkout_ScansAnItem_WhenItemAddedAtCheckout()
        {
            //arrange
            var item = new Item();
            var scanner = new Mock<IScanner>();

            var checkout = new Checkout(scanner.Object);

            //act
            checkout.AddItem(item);

            //assert
            scanner.Verify(s => s.Scan(item), Times.Once());
        }

        [Fact]
        public void Checkout_ReturnsTotalPrice_WhenRequested()
        {
            //arrange
            var item1 = new Item { Price = 1.75 };
            var item2 = new Item { Price = 0.75 };

            var scanner = new Mock<IScanner>();

            var checkout = new Checkout(scanner.Object);

            //act
            checkout.AddItem(item1);
            checkout.AddItem(item2);

            //assert
            Math.Round(checkout.GetTotalPrice(), 2).Should().Be(2.5);
        }
    }
}
