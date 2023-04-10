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

        public ViewResult Index(int productPage = 1) //If its called without parameter, display products for the first page
            => View(new ProductsListViewModel 
            {
                Products = repository.Products
                .OrderBy(p => p.ProductID)
                .Skip((productPages -1) * PageSize) //Skip the products that occur before the start of the current page
                .Take(PageSize),
                PagingInfo = new PagingInfo 
                {
                    CurrentPage= productPages,
                    ItemsPerPage=PageSize,
                    TotalItems = repository.Products.Count()
                }
            }); //Take the number of products specified by PageSize field
    }
}
