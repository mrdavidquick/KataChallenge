using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
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
            var specialOfferService = new Mock<ISpecialOfferService>();
            var checkout = new Checkout(scanner.Object, specialOfferService.Object);

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
            var specialOfferService = new Mock<ISpecialOfferService>();
            var checkout = new Checkout(scanner.Object, specialOfferService.Object);

            //act
            checkout.AddItem(item1);
            checkout.AddItem(item2);

            //assert
            Math.Round(checkout.GetTotalPrice(), 2).Should().Be(2.5);
        }

        [Fact]
        public void Checkout_AppliesSpecialOffers_WhenApplicable()
        {
            //arrange
            var item1 = new Item { Price = 0.3 };
            var item2 = new Item { Price = 0.5 };
            var item3 = new Item { Price = 0.3 };

            var scanner = new Mock<IScanner>();
            var specialOfferService = new Mock<ISpecialOfferService>();
            specialOfferService.Setup(s => s.GetSpecialOfferDiscount(It.IsAny<IReadOnlyCollection<Item>>()))
                .Returns(0.15);

            var checkout = new Checkout(scanner.Object, specialOfferService.Object);

            //act
            checkout.AddItem(item1);
            checkout.AddItem(item2);
            checkout.AddItem(item3);

            //assert
            Math.Round(checkout.GetTotalPriceIncludingDiscounts(), 2).Should().Be(0.95);
        }
    }
}
