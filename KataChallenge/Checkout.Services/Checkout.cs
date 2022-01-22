namespace Checkout.Services
{
    public class Checkout : ICheckout
    {
        private readonly IScanner _scanner;

        public Checkout(IScanner scanner)
        {
            _scanner = scanner;
        }

        public void AddItem(Item item)
        {
            _scanner.Scan(item);
        }
    }
}
