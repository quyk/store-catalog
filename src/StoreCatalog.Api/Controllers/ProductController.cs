using Microsoft.AspNetCore.Mvc;
using StoreCatalog.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreCatalog.Api.Controllers
{
    [Route("api/products")]
    public class ProductController : Controller
    {
        [HttpGet]
        public async Task<ActionResult<ProductModel>> GetProduct()
        {
            var product = new ProductModel
            {
                StoreId = new Guid("770E97A9-D76A-45BA-B5BC-3A1BF4C16CBC"),
                ProductId = new Guid("86FA6CA2-6F26-42BE-8240-1EA812B92D49"),
                Name = "Darth Bacon",
                Image = "img_db.jpg",
                Items = new List<ItemModel>
                {
                    new ItemModel
                    {
                        ItemId = new Guid("B309B06B-D93C-452C-8F44-0EFE1A390A1A"),
                        Name = "Bread"
                    },
                    new ItemModel
                    {
                        ItemId = new Guid("D3C4F9CF-8F84-43FC-9212-A8477714F351"),
                        Name = "Meat"
                    }
                },
                Price = "10.20"
            };

            return new OkObjectResult(product);
        }
    }
}
