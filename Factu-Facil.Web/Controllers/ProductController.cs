using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactuFacil.Entity;
using FactuFacil.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FactuFacil.Web.Util;
using FactuFacil.Web.Models;

namespace FactuFacil.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService _productRepository;

        public ProductController(IProductService productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return Ok(await _productRepository.GetAll(p => true, i => i.CreatedBy, i => i.UpdatedBy));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> Get(Guid id)
        {
            try
            {
                return Ok(await _productRepository.GetOne(p => true, i => i.CreatedBy, i => i.UpdatedBy));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> Create([FromBody]ProductViewModel model)
        {
            try
            {
                var product = new Product();
                product.Id = Guid.NewGuid();
                product.Name = model.Name;
                product.Description = model.Description;
                product.SalePrice = model.SalePrice;
                product.PurchasePrice = model.PurchasePrice;
                product.Code = model.Code;
                product.CreatedAt = DateTime.Now;
                product.CreatedById = GetUser().Id;
                product.UpdatedAt = DateTime.Now;
                product.UpdatedById = GetUser().Id;

                await _productRepository.Add(product);
                return CreatedAtAction(nameof(Get), new { product.Id });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> Update([FromBody]ProductViewModel model)
        {
            try
            {
                var product = await _productRepository.GetOne(x => x.Id == model.Id);
                product.Name = model.Name;
                product.Description = model.Description;
                product.SalePrice = model.SalePrice;
                product.PurchasePrice = model.PurchasePrice;
                product.Code = model.Code;
                product.UpdatedAt = DateTime.Now;
                product.UpdatedById = GetUser().Id;
                await _productRepository.Update(product);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> Dete(Guid id)
        {
            try
            {
                var entity = await _productRepository.GetOne(p => p.Id == id);
                await _productRepository.Delete(entity);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
