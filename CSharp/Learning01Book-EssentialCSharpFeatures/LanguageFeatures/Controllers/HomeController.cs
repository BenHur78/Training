using Microsoft.AspNetCore.Mvc;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() {

            //We provide to the view an array of string's
            return View(new string[] { "C#", "Language", "Features" });
        }
    }
}
