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
    public class ClientController : BaseController
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Client>>> Get()
        {
            return Ok(await _clientService.GetAll(p => true, i => i.CreatedBy, i => i.UpdatedBy));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Client>> Get(Guid id)
        {
            try
            {
                return Ok(await _clientService.GetOne(p => p.Id == id, i => i.CreatedBy, i => i.UpdatedBy));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Client>> Create([FromBody]ClientViewModel model)
        {
            try
            {
                Client client = new Client();

                client.Id = Guid.NewGuid();
                client.Name = model.Name;
                client.LastName = model.LastName;
                client.IdentificationCard = model.IdentificationCard;
                client.Phone = model.Phone;
                client.Address = model.Address;

                client.CreatedAt = DateTime.Now;
                client.CreatedById = GetUser().Id;

                client.UpdatedAt = DateTime.Now;
                client.UpdatedById = GetUser().Id;

                await _clientService.Add(client);
                return CreatedAtAction(nameof(Get), new { client.Id });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Client>> Update([FromBody]ClientViewModel model)
        {
            try
            {
                Client client = await _clientService.GetOne(p => p.Id == model.Id);
                client.Name = model.Name;
                client.LastName = model.LastName;
                client.IdentificationCard = model.IdentificationCard;
                client.Phone = model.Phone;
                client.Address = model.Address;
                client.UpdatedAt = DateTime.Now;
                client.UpdatedById = GetUser().Id;

                await _clientService.Update(client);
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
        public async Task<ActionResult<Client>> Delete(Guid id)
        {
            try
            {
                var entity = await _clientService.GetOne(p => p.Id == id);
                await _clientService.Delete(entity);
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
