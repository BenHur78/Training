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

        [Fact]
        public void SquareIsARectangle_ThenIShouldBeAble_ToReplaceARectangleWithASquare_AndItShouldBehaveTheSameWayAsTheRectangle()
        {
            // Arrange
            var rectangle1 = new Rectangle(2, 3);
            var recArea = Functions.Area(rectangle1);
            recArea.Should().Be(6);

            var square = new Square(); //we should be able to cast Square to Rectangle without any issues.
            square.Width = 4;
            var squareArea = Functions.Area(square);
            squareArea.Should().Be(16);

            // Act
            Rectangle rectangle2 = new Square(); // Liskov Substitution Principle fixed
            rectangle2.Width = 4;
            recArea = Functions.Area(rectangle2);

            // Assert
            recArea.Should().Be(16);
        }
    }
}