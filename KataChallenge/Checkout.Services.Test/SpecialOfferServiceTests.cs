using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Checkout.Services.Test
{
    public class SpecialOfferServiceTests
    {
        [Fact]
        public void GetSpecialOfferDiscount_DoesntApplyDiscount_WhenDiscountsAreNotApplicable()
        {
            //arrange
            var specialOfferService = new SpecialOfferService();

            var items = new List<Item>
            {
                new Item { SKU = "F23", Price = 0.2 },
                new Item { SKU = "F23", Price = 0.5 },
                new Item { SKU = "G66", Price = 0.3 }
            };

            //act
            var discount = specialOfferService.GetSpecialOfferDiscount(items);

            //assert
            Math.Round(discount, 2).Should().Be(0);
        }

        [Fact]
        public void GetSpecialOfferDiscount_AppliesAppropriateDiscount_WhenDiscountsAreApplicable()
        {
            //arrange
            var specialOfferService = new SpecialOfferService();

            var items = new List<Item>
            {
                new Item { SKU = "B15", Price = 0.3 },
                new Item { SKU = "A99", Price = 0.5 },
                new Item { SKU = "B15", Price = 0.3 }
            };

            //act
            var discount = specialOfferService.GetSpecialOfferDiscount(items);

            //assert
            Math.Round(discount, 2).Should().Be(0.15);
        }

        [Fact]
        public void GetSpecialOfferDiscount_AppliesMultipleDiscounts_WhenMultipleDiscountsAreApplicable()
        {
            //arrange
            var specialOfferService = new SpecialOfferService();

            var items = new List<Item>
            {
                new Item { SKU = "B15", Price = 0.3 },
                new Item { SKU = "A99", Price = 0.5 },
                new Item { SKU = "B15", Price = 0.3 },
                new Item { SKU = "B15", Price = 0.3 },
                new Item { SKU = "B15", Price = 0.3 },
                new Item { SKU = "B15", Price = 0.3 }
            };

            //act
            var discount = specialOfferService.GetSpecialOfferDiscount(items);

            //assert
            Math.Round(discount, 2).Should().Be(0.3);
        }
    }
}
