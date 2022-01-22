using System.Collections.Generic;
using System.Linq;

namespace Checkout.Services
{
    public class Checkout : ICheckout
    {
        private readonly IScanner _scanner;
        private List<Item> _scannedItems = new List<Item>();

        public Checkout(IScanner scanner)
        {
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
    }
}
