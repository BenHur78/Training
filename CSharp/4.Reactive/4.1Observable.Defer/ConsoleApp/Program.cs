// See https://learn.microsoft.com/en-us/previous-versions/dotnet/reactive-extensions/hh229160(v=vs.103) for more information
using System.Reactive.Linq; // Required for Observable methods

//*****************************************************************************************************//
//*** Product inventories change from time to time. This example demonstrates the Defer operator    ***//
//*** by creating an observable sequence of the Product class. The creation of the sequence is      ***//
//*** deferred until the observer calls Subscribe and a new observable sequence is always generated ***//
//*** at that time with the latest inventory levels to be sent to the observer.                     ***//
//*****************************************************************************************************//
ProductInventory myInventory = new ProductInventory();
IObservable<Product> productObservable = Observable.Defer(myInventory.GetUpdatedInventory);

//******************************************************//
//*** Generate a simple table in the console window. ***//
//******************************************************//
Console.WriteLine("Current Inventory...\n");
Console.WriteLine("\n{0,-13} {1,-37} {2,-18}", "Product Name", "Product ID", "Current Inventory");
Console.WriteLine("{0,-13} {1,-37} {2,-18}", "============", "====================================",
    "=================");

//**********************************************************************************//
//*** Each product in the sequence will be reported in the table using the       ***//
//*** Observer's OnNext handler provided with the Subscribe method.              ***//
//**********************************************************************************//
productObservable.Subscribe(prod => Console.WriteLine(prod.ToString()));

//******************************************************************************************************//
//*** To get the updated sequence from the deferred observable all we have to do is subscribe again. ***//
//******************************************************************************************************//
Console.WriteLine("\n\nThe updated current Inventory...\n");
Console.WriteLine("\n{0,-13} {1,-37} {2,-18}", "Product Name", "Product ID", "Current Inventory");
Console.WriteLine("{0,-13} {1,-37} {2,-18}", "============", "====================================",
    "=================");

productObservable.Subscribe(prod => Console.WriteLine(prod.ToString()));


Console.WriteLine("\nPress ENTER to exit...\n");
Console.ReadLine();

//**************************************************************************************************//
//***                                                                                            ***//
//*** The Product class holds current product inventory information and includes the ability for ***//
//*** each product to display its information to the console window.                             ***//
//***                                                                                            ***//
//**************************************************************************************************//
class Product
{
    private readonly string productName;
    private readonly string productID;
    private int currentInventory;

    public Product(string name, int inventory)
    {
        productName = name;
        productID = Guid.NewGuid().ToString();
        currentInventory = inventory;
    }

    public void RemoveInventory(int delta)
    {
        currentInventory -= delta;

        if (currentInventory < 0)
            currentInventory = 0;
    }

    public override string ToString()
    {
        return String.Format("{0,-13} {1,-37} {2,-18}", productName, productID, currentInventory);
    }
}

//*****************************************************************************************************//
//***                                                                                               ***//
//*** The ProductInventory class initializes all our product information and provides an Observable ***//
//*** sequence of the product inventories through the GetUpdatedInventory() method. This method     ***//
//*** is provided to our call to Observable.Defer() so that all subscriptions against the deferred  ***//
//*** observable get the lastest inventory information whenever Subscribe is called.                ***//
//***                                                                                               ***//
//*****************************************************************************************************//
class ProductInventory
{
    private Product[] products = new Product[5];
    private Random random = new Random();

    public ProductInventory()
    {
        for (int i = 0; i < 5; i++)
        {
            //*************************************************************//
            //*** Initial inventories will be a random count under 1000 ***//
            //*************************************************************//
            products[i] = new Product("Product " + (i + 1).ToString(), random.Next(1000));
        }
    }

    public IObservable<Product> GetUpdatedInventory()
    {
        //***************************************************************************************************//
        //*** When inventory for each product is updated up to 50 of each product is consumed or shipped. ***//
        //***************************************************************************************************//
        for (int i = 0; i < 5; i++)
            products[i].RemoveInventory(random.Next(51));

        //****************************************************************************************************//
        //*** This updated observable sequence is always provided by this method when Subscribe is called. ***//
        //****************************************************************************************************//
        IObservable<Product> updatedProductSequence = products.ToAsyncEnumerable().ToObservable();
        return updatedProductSequence;
    }
}