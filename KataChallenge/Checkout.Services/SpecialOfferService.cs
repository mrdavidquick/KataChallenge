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
            var itemsWithSpecialOffers = specialOffers.Where(o => items.Any(i => i.SKU == o.SKU));
            foreach (var offer in itemsWithSpecialOffers)
            {
                var matchingItems = items.Where(i => i.SKU == offer.SKU).ToList();
                var matchingCount = matchingItems.Count;
                if (!(matchingCount >= offer.QualifyingQuantity)) continue;

                var remainder = 0;
                var numberOfDiscounts = Math.DivRem(matchingCount, offer.QualifyingQuantity, out remainder);

                var itemDiscount = ((numberOfDiscounts * offer.QualifyingQuantity) * matchingItems.First().Price) - (numberOfDiscounts * offer.OfferPrice);

                totalDiscount += itemDiscount;
            }

            return totalDiscount;
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