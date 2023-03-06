using System;
using System.Collections.Generic;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers   
{
    [ApiController] //this brings default behaviors to this class that makes our life easier
    //[Route("[controller]")] //define the route to which this controller will responding. This means GET /items
    [Route("items")] //define the route to which this controller will responding. This means GET /items
    public class ItemsController : ControllerBase
    {
        private readonly InMemItemsRepository repository;

        public ItemsController()
        {
            repository = new InMemItemsRepository();
        }

        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            var items = repository.GetItems();
            
            return items;
        }

        [HttpGet("{id}")] // GET /items/{id}
        public ActionResult<Item> GetItem(Guid id)
        {
            var item = repository.GetItem(id);

            if(item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
}