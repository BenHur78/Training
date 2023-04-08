namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() {

            ShoppingCart cart = new () { Products = Product.GetProducts() };

            Product[] productArray = {
                new Product { Name = "Kayak", Price = 275M},
                new Product { Name = "Lifejacked", Price = 48.95M},
                new Product { Name = "Soccer Ball", Price = 19.50M},
                new Product { Name = "Corner flag", Price = 34.95M},
            };

            decimal arrayTotal = productArray.FilterByPrice(20).TotalPrices();

            return View("Index", new string[] { $"Array Total: {arrayTotal:C2}" });
        }
    }
}
