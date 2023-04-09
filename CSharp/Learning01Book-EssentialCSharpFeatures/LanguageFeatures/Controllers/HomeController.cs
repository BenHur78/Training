namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            var names = new[] { "Kayak", "Lifejacket", "Soccler ball" };
            
            return View(names);
        }
    }
}
