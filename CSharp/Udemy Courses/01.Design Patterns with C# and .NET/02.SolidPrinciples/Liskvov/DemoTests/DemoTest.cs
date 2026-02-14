using DesignPatterns;
using FluentAssertions;

namespace DemoTests
{
    public class DemoTest
    {
        [Fact]
        public void ShouldBeRectangle()
        {
            // Arrange
            var rectangle = new Rectangle(2, 3);

            // Act
            var result = Functions.Area(rectangle);

            // Assert
            result.Should().Be(6);
        }

        [Fact]
        public void ShouldBeSquare()
        {
            // Arrange
            var square = new Square();
            square.Width = 4;

            // Act
            var result = Functions.Area(square);

            // Assert
            result.Should().Be(16);
        }
    }
}