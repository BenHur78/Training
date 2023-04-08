namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() {

            Product?[] products = Product.GetProducts();
            
            Product? product= products[0];
            string val;
            //if(val == null)
            //{
            //    Console.Error.WriteLine("Val is null");
            //}
            //else
            //{
            //    Console.Error.WriteLine("Val is not null");
            //}

            if(product != null)
            {
                val= product.Name;
            }
            else
            {
                val = "No value";
            }

            return View(new string[] { val });
        }
    }
}
