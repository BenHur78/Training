namespace DesignPatterns
{
    public enum Color
    {
        Red,
        Green,
        Blue
    }

    public enum Size
    {
        Small,
        Medium,
        Large,
        Yuge
    }

    public class Product
    {
        public string Name;

        public Color Color;

        public Size Size;

        public Product(string name, Color color, Size size)
        {
            if (name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }
            Name = name;
            Color = color;
            Size = size;
        }

        public class ProductFilter
        {
            public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
            {
                foreach(var p in products)
                {
                    if (p.Size == size)
                    {
                        yield return p;
                    }
                }
            }

            public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
            {
                foreach (var p in products)
                {
                    if (p.Color == color)
                    {
                        yield return p;
                    }
                }
            }

            //Here we had to reopen ProductFilter
            public IEnumerable<Product> FilterByColorAndSize(IEnumerable<Product> products, Color color, Size size)
            {
                foreach (var p in products)
                {
                    if (p.Color == color && p.Size == size)
                    {
                        yield return p;
                    }
                }
            }
        }

    }

    // A specification of T
    public interface ISpecification<T>
    {
        bool IsStatisfied(T t);
    }

    // We take a bunch of items of type T and we will filter them according to a specification
    interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    // Let assume we want to filter items by Color
    public class ColorSpecification : ISpecification<Product>
    {
        private Color _color;

        public ColorSpecification(Color color)
        {
            _color = color;
        }

        public bool IsStatisfied(Product t)
        {
            return t.Color == _color;
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach(var item in items)
            {
                if(spec.IsStatisfied(item))
                {
                    yield return item;
                }
            }
        }
    }

    public class Demo
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };

            var pf = new Product.ProductFilter();
            Console.WriteLine("Green products (old):");

            foreach(var p in pf.FilterByColor(products, Color.Green))
            {
                Console.WriteLine($" - {p.Name} is green");
            }

            var betterFilter = new BetterFilter();
            Console.WriteLine("Green products (new):");

            foreach(var p in betterFilter.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($" - {p.Name} is green");
            }
        }
    }

}