# Steps
- Creating Rectangle class with a Width and Height.
- Creating a Square class. A Square is a Rectangle with equal Width and Height.
- Violating the Liskov Substitution Principle. 
- 
# What we learned here
- The Liskov Substitution Principle (LSP) states that objects of a subclasse should be replaceable with objects 
of its superclass without breaking the application or altering expected behavior. So if square inherits from rectangle,
it means that square is a rectangle, so we should be able to use a square as a rectangle.
- This means that derived classes like Square, they extend functionality of the base class, they do not change
base functionality. In other words, derived classes should behave the same way as their parent.