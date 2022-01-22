using Moq;
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

            var checkout = new Checkout(scanner.Object);

            //act
            checkout.AddItem(item);

            //assert
            scanner.Verify(s => s.Scan(item), Times.Once());
        }
    }
}
