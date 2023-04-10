using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository repository;

        public int PageSize = 4; //I want this number of products per page

        public HomeController(IStoreRepository repo)
        {
            repository= repo;
        }

        public IActionResult Index(int productPage = 1) //If its called without parameter, display products for the first page
            => View(repository.Products
                .OrderBy(p => p.ProductID)
                .Skip((productPage -1) * PageSize) //Skip the products that occur before the start of the current page
                .Take(PageSize)); //Take the number of products specified by PageSize field
    }
}
