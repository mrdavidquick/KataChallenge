using System.Collections.Generic;
using System.Linq;

namespace Checkout.Services
{
    public class Checkout : ICheckout
    {
        private readonly IScanner _scanner;
        private List<Item> _scannedItems = new List<Item>();
        private readonly ISpecialOfferService _specialOfferService;

        public Checkout(IScanner scanner, ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
            _scanner = scanner;
        }

        public void AddItem(Item item)
        {
            _scanner.Scan(item);
            _scannedItems.Add(item);
        }

        public double GetTotalPrice()
        {
            return _scannedItems.Sum(i => i.Price);
        }

        public double GetTotalPriceIncludingDiscounts()
        {
            return GetTotalPrice() - _specialOfferService.GetSpecialOfferDiscount(_scannedItems);
        }
    }
}
