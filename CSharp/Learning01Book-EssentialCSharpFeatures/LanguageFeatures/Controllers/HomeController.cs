namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ViewResult> Index()
        {
            long? length = await MyAsincMethods.GetPageLength();
            
            return View(new string[] { $"Length: {length}" });
        }
    }
}
