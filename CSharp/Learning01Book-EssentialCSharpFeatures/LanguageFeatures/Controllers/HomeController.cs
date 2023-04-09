namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ViewResult> Index()
        {
            List<string> output = new List<string>();

            foreach(long? len in await MyAsincMethods.GetPageLengths(output, "apress.com", "microsoft.com", "amazon.com"))
            {
                output.Add($"Page length: { len }");
            }

            return View(output);
        }
    }
}
