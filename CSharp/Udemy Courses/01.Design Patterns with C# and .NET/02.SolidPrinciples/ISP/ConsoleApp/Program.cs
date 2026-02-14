namespace DesignPatterns
{
    public class Document
    {

    }

    public interface IMachine
    {
        void Print(Document d);
        void Scan(Document d);
        void Fax(Document d);
    }

    public class MultiFunctionMachine : IMachine
    {
        public void Print(Document d)
        {
            Console.WriteLine("Printing document...");
        }
        public void Scan(Document d)
        {
            Console.WriteLine("Scanning document...");
        }
        public void Fax(Document d)
        {
            Console.WriteLine("Faxing document...");
        }
    }

    public class OldFashionedPrinter : IMachine
    {
        public void Print(Document d)
        {
            Console.WriteLine("Printing document...");
        }
        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }
        public void Fax(Document d)
        {
            throw new NotImplementedException();
        }
    }

    public class Demo
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"");
        }
    }

}