namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() {

            Product[] products = Product.GetProducts();
            //We provide to the view an array of string's
            return View(new string[] { products[0].Name });
        }
    }
}
