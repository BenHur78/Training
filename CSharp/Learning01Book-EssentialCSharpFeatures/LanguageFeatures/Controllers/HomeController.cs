namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() {

            ShoppingCart cart = new () { Products = Product.GetProducts() };

            Product[] productArray = {
                new Product { Name = "Kayak", Price = 275M},
                new Product { Name = "Lifejacked", Price = 48.95M},
            };

            decimal cartTotal = cart.TotalPrices();
            decimal arrayTotal = productArray.TotalPrices();

            return View("Index", new string[] { $"Cart Total: {cartTotal:C2}", $"Array Total: {arrayTotal:C2}" });
        }
    }
}
