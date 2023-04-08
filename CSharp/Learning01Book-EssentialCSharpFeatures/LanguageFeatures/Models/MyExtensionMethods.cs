namespace LanguageFeatures.Models
{
    public static class MyExtensionMethods
    {
        public static decimal TotalPrices(this IEnumerable<Product?> products)
        {
            decimal total = 0;

            foreach (Product? prod in products)
            {
                total += prod?.Price ?? 0;
            }

            return total;
        }

        public static IEnumerable<Product?> FilterByPrice(this IEnumerable<Product?> productsEnum, decimal minimumPrice)
        {
            foreach(Product? prod in productsEnum)
            {
                if((prod?.Price ?? 0) >= minimumPrice)
                {
                    yield return prod;
                }
            }
        }

        public static IEnumerable<Product?> FilterByName(this IEnumerable<Product?> productsEnum, char firstLetter)
        {
            foreach (Product? prod in productsEnum)
            {
                if (prod?.Name?[0] == firstLetter)
                {
                    yield return prod;
                }
            }
        }

        public static IEnumerable<Product?> Filter(this IEnumerable<Product?> productsEnum, Func<Product?, bool> selector)
        {
            foreach(Product? product in productsEnum)
            {
                if(selector(product))
                {
                    yield return product;
                }
            }
        }

    }
}
