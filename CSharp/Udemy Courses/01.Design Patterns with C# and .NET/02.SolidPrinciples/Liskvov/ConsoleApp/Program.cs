namespace DesignPatterns
{
    public class Rectangle
    {

        public int Width { get; set; }

        public int Height { get; set; }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Functions
    {
        public static int Area(Rectangle r) => r.Width * r.Height;
    }

    public class Demo
    {
        static void Main(string[] args)
        {
            var rc = new Rectangle(2, 3);
            Console.WriteLine($"{rc} has area {Functions.Area(rc)}");
        }
    }

}