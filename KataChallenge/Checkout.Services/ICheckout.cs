namespace Checkout.Services
{
    public interface ICheckout
    {
        void AddItem(Item item);
        double GetTotalPrice();
    }
}