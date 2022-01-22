using System.Collections.Generic;

namespace Checkout.Services
{
    public interface ISpecialOfferService
    {
        double GetSpecialOfferDiscount(IReadOnlyCollection<Item> items);
    }
}
