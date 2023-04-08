namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() {

            Product?[] products = Product.GetProducts();
            
            //if(val == null)
            //{
            //    Console.Error.WriteLine("Val is null");
            //}
            //else
            //{
            //    Console.Error.WriteLine("Val is not null");
            //}

            return View(new string[] { products[0]?.Name ?? "No Value" });
        }
    }
}
