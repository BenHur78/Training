using DesignPatterns;
using FluentAssertions;

namespace DemoTests
{
    public class DemoTest
    {
        [Fact]
        public void ShouldBeAbleToFilterBySize()
        {
            // Arrange
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };
            var sut = new Product.ProductFilter();

            // Act
            var result = sut.FilterBySize(products, Size.Large);

            // Assert
            result.Count().Should().Be(2);
        }

        [Fact]
        public void ShouldBeAbleToFilterByColor()
        {
            // Arrange
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };
            var sut = new Product.ProductFilter();

            // Act
            var result = sut.FilterByColor(products, Color.Green);

            // Assert
            result.Count().Should().Be(2);
        }
    }
}