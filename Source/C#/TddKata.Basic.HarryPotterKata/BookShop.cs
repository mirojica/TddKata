using System.Collections.Generic;
using System.Linq;

namespace TddKata.Basic.HarryPotterKata
{
    public class BookShop
    {
        private const int BookPrice = 8;

        private readonly IReadOnlyDictionary<int, int> _discountByNumberOfBooks = new Dictionary<int, int>
        {
            {1, 0}, {2, 5}, {3, 10}, {4, 20}, {5, 25}
        };

        public decimal Sell(params string[] books)
        {
            decimal totalPrice = 0;

            var bookAppearance = books.GroupBy(book => book)
                .ToDictionary(groupOfBooks => groupOfBooks.Key, groupOfBooks => groupOfBooks.Count());

            var maximumBookAppearance = bookAppearance.Values.Max();

            for (var i = 0; i < maximumBookAppearance; i++)
            {
                var numberOfBooks = bookAppearance.Count(book => book.Value > i);
                var priceWithoutDiscount = numberOfBooks * BookPrice;
                totalPrice += priceWithoutDiscount - Discount(numberOfBooks, priceWithoutDiscount);
            }

            return totalPrice;
        }

        private decimal Discount(int numberOfBooks, int priceWithoutDiscount)
        {
            return (decimal)(priceWithoutDiscount * _discountByNumberOfBooks[numberOfBooks]) / 100;
        }
    }
}