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
    public class InventoryController : BaseController
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Inventory>>> Get()
        {
            var inventory = await _inventoryService.GetAll(p => true);
            return Ok(inventory);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Inventory>> Get(Guid id)
        {
            try
            {
                return Ok(await _inventoryService.GetOne(p => p.Id == id));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Inventory>> Create([FromBody]InventoryViewModel model)
        {
            try
            {
                var inventory = new Inventory();

                inventory.Id = Guid.NewGuid();
                inventory.Amount = model.Amount;
                inventory.ProductId = model.ProductId;
                inventory.CreatedAt = DateTime.Now;
                inventory.CreatedById = GetUser().Id;
                inventory.UpdatedAt = DateTime.Now;
                inventory.UpdatedById = GetUser().Id;

                await _inventoryService.Add(inventory);
                return CreatedAtAction(nameof(Get), new { inventory.Id });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Inventory>> Update([FromBody]InventoryViewModel model)
        {
            try
            {
                var inventory = await _inventoryService.GetOne(x => x.Id == model.Id);
                inventory.Amount = model.Amount;
                inventory.ProductId = model.ProductId;
                inventory.UpdatedAt = DateTime.Now;
                inventory.UpdatedById = GetUser().Id;

                await _inventoryService.Update(inventory);
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
        public async Task<ActionResult<Inventory>> Delete(Guid id)
        {
            try
            {
                var entity = await _inventoryService.GetOne(p => p.Id == id);
                await _inventoryService.Delete(entity);
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
