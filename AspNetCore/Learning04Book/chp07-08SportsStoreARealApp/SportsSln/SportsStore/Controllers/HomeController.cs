using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

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

        public ViewResult Index(string? category, int productPage = 1) //If its called without parameter, display products for the first page
            => View(new ProductsListViewModel 
            {
                Products = repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((productPage -1) * PageSize) //Skip the products that occur before the start of the current page
                .Take(PageSize),
                PagingInfo = new PagingInfo 
                {
                    CurrentPage= productPage,
                    ItemsPerPage=PageSize,
                    TotalItems = category == null 
                    ? repository.Products.Count() 
                    : repository.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            }); //Take the number of products specified by PageSize field
    }
}
