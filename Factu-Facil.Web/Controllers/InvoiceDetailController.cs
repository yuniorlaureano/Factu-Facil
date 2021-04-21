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
    public class InvoiceDetailController : BaseController
    {
        private readonly IInvoiceDetailService _invoiceDetailService;

        public InvoiceDetailController(IInvoiceDetailService invoiceDetailService)
        {
            _invoiceDetailService = invoiceDetailService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<InvoiceDetail>>> Get()
        {
            return Ok(await _invoiceDetailService.GetAll(p => true, i => i.CreatedBy, i => i.UpdatedBy));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InvoiceDetail>> Get(Guid id)
        {
            try
            {
                return Ok(await _invoiceDetailService.GetOne(p => true, i => i.CreatedBy, i => i.UpdatedBy));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InvoiceDetail>> Create([FromBody]InvoiceDetailViewModel model)
        {
            try
            {
                var invoiceDetail = new InvoiceDetail();
                invoiceDetail.Id = Guid.NewGuid();
                invoiceDetail.Quantity = model.Quantity;
                invoiceDetail.Price = model.Price;
                invoiceDetail.ProductId = model.ProductId;
                invoiceDetail.InvoiceId = model.InvoiceId;
                invoiceDetail.CreatedAt = DateTime.Now;
                invoiceDetail.CreatedById = GetUser().Id;
                invoiceDetail.UpdatedAt = DateTime.Now;
                invoiceDetail.UpdatedById = GetUser().Id;

                await _invoiceDetailService.Add(invoiceDetail);
                return CreatedAtAction(nameof(Get), new { invoiceDetail.Id });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InvoiceDetail>> Update([FromBody]InvoiceDetailViewModel model)
        {
            try
            {
                var invoiceDetail = await _invoiceDetailService.GetOne(x => x.Id == model.Id);
                invoiceDetail.Quantity = model.Quantity;
                invoiceDetail.Price = model.Price;
                invoiceDetail.ProductId = model.ProductId;
                invoiceDetail.InvoiceId = model.InvoiceId;
                invoiceDetail.UpdatedAt = DateTime.Now;
                invoiceDetail.UpdatedById = GetUser().Id;

                await _invoiceDetailService.Update(invoiceDetail);
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
        public async Task<ActionResult<InvoiceDetail>> Delete(Guid id)
        {
            try
            {
                var entity = await _invoiceDetailService.GetOne(p => p.Id == id);
                await _invoiceDetailService.Delete(entity);
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
