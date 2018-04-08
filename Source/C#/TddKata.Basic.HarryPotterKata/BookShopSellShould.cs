using FluentAssertions;
using Xunit;

namespace TddKata.Basic.HarryPotterKata
{
    public class BookShopSellShould
    {
        [Fact]
        public void CalculatePriceWithoutDiscount_WhenSellingOneBook()
        {
            var bookShop = new BookShop();

            var actualPrice = bookShop.Sell("first book");

            actualPrice.Should().Be(8);
        }

        [Theory]
        [InlineData(15.2, "first book", "second book")]
        [InlineData(21.6, "first book", "second book", "third book")]
        [InlineData(25.6, "first book", "second book", "third book", "fourth book")]
        [InlineData(30, "first book", "second book", "third book", "fourth book", "fifth book")]
        public void CalculatePriceWithApropriateDiscount_WhenSellingMultipleDifferentBooks(decimal expectedPrice, params string[] books)
        {
            var bookShop = new BookShop();

            var actualPrice = bookShop.Sell(books);

            actualPrice.Should().Be(expectedPrice);
        }

        [Fact]
        public void CalculatePriceWithoutDiscount_WhenAllBooksAreTheSame()
        {
            var bookShop = new BookShop();

            var actualPrice = bookShop.Sell("first book", "first book", "first book");

            actualPrice.Should().Be(24);
        }

        [Theory]
        [InlineData(23.2, "first book", "second book", "first book")]
        [InlineData(44.8, "first book", "second book", "first book", "second book", "first book", "third book")]
        [InlineData(51.6, "first book", "second book", "third book", "first book", "second book", "third book", "fourth book", "fifth book")]
        public void CalculatePriceWithApropriateDiscount_WhenBooksAreMixed(decimal expectedPrice, params string[] books)
        {
            var bookShop = new BookShop();

            var actualPrice = bookShop.Sell(books);

            actualPrice.Should().Be(expectedPrice);
        }
    }
}