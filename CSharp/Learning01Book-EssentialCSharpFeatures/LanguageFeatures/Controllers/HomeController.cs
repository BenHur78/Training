namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() {

            Product?[] products = Product.GetProducts();
            
            return View(new string[] { $"Name: {products[0]?.Name}, Price: {products[1]?.Price:C2}" } );
        }
    }
}
