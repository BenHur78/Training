namespace DesignPatterns
{
    public class Rectangle
    {

        public int Width { get; set; }

        public int Height { get; set; }

        public Rectangle(int width = 0, int height = 0)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle
    {
        public new int Width 
        { 
            set
            {
                base.Width = base.Height = value;
            } 
        }

        public new int Height 
        {
            set
            {
                base.Width = base.Height = value;
            }
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

            var square = new Square();
            square.Width = 4;
            Console.WriteLine($"{square} has area {Functions.Area(square)}");   
        }
    }

}