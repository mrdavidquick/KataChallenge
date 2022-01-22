using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkout.Services
{
    public class SpecialOfferService : ISpecialOfferService
    {
        public double GetSpecialOfferDiscount(IReadOnlyCollection<Item> items)
        {
            var totalDiscount = 0.00;
            var specialOffers = GetSpecialOffers();
            var itemsWithSpecialOffers = GetItemsWithSpecialOffers(items, specialOffers);
            foreach (var offer in itemsWithSpecialOffers)
            {
                var matchingItems = items.Where(i => i.SKU == offer.SKU).ToList();
                var matchingCount = matchingItems.Count;
                if (!(matchingCount >= offer.QualifyingQuantity)) continue;

                var remainder = 0;
                var numberOfDiscountsToApply = Math.DivRem(matchingCount, offer.QualifyingQuantity, out remainder);

                var itemDiscount = GetItemDiscount(numberOfDiscountsToApply, matchingItems.First().Price, numberOfDiscountsToApply, offer.OfferPrice, offer.QualifyingQuantity);

                totalDiscount += itemDiscount;
            }

            return totalDiscount;
        }

        private static IEnumerable<SpecialOffer> GetItemsWithSpecialOffers(IReadOnlyCollection<Item> items, IEnumerable<SpecialOffer> specialOffers)
        {
            return specialOffers.Where(o => items.Any(i => i.SKU == o.SKU));
        }

        private static double GetItemDiscount(int numberOfDiscounts, double originalItemCost,
            int numberOfDiscountsToApply, double discountPrice, int qualifyingQuantity)
        {
            var itemsBeingDiscounted = numberOfDiscounts * qualifyingQuantity;
            return (itemsBeingDiscounted * originalItemCost) - (numberOfDiscountsToApply * discountPrice);
        }

        private IEnumerable<SpecialOffer> GetSpecialOffers()
        {
            return new List<SpecialOffer>
            {
                new SpecialOffer { SKU = "A99", QualifyingQuantity = 3, OfferPrice = 1.30 },
                new SpecialOffer { SKU = "B15", QualifyingQuantity = 2, OfferPrice = 0.45 }
            };
        }
    }
}