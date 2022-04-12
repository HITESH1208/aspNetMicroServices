using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("app/v1/[Controller]")]
    public class CatalogController : Controller
    {
        private readonly IProductRepository _repo;
        private readonly ILogger<CatalogController> _log;
        public CatalogController(IProductRepository repo, ILogger<CatalogController> log)
        {
            _repo = repo;
            _log = log;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProduct()
        {
            var product = await _repo.GetAllProducts();
            return Ok(product);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Route("{id:length(24)}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _repo.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Route("[action]/{name}", Name = "GetProductByName")]
        public async Task<ActionResult<Product>> GetProductByName(string name)
        {
            var product = await _repo.GetProductByName(name);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Route("[action]/{category}", Name = "GetProductByCategory")]
        public async Task<ActionResult<Product>> GetProductByCategory(string category)
        {
            var product = await _repo.GetProductByCategory(category);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _repo.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id, product });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _repo.UpdateProduct(product));
        }

        [HttpDelete]
        [Route("{id:length(24)}",Name ="DeleteProduct")]
        public async Task<ActionResult<Product>> DeleteProduct(string id)
        {
            return Ok(await _repo.DeleteProduct(id));
        }

    }
}
