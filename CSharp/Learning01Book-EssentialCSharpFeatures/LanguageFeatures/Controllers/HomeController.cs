namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() {

            ShoppingCart cart = new () { Products = Product.GetProducts() };
            decimal cartTotal = cart.TotalPrices();
            
            return View("Index", new string[] { $"Total: {cartTotal:C2}" });
        }
    }
}
