namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            var products = new[]
            {
                new { MyName = "Kayak", MyPrice = 275M },
                new { MyName = "Lifejacket", MyPrice = 48.95M },
                new { MyName = "Soccer ball", MyPrice = 19.50M },
                new { MyName = "Corner flag", MyPrice = 34.95M },
            };
            
            return View(products.Select(p => p.GetType().ToString()));
        }
    }
}
