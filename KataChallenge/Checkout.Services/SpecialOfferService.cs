using System;
using System.Collections.Generic;

namespace Checkout.Services
{
    internal class SpecialOfferService : ISpecialOfferService
    {
        public double GetSpecialOfferDiscount(IReadOnlyCollection<Item> items)
        {
            throw new NotImplementedException();
        }
    }
}