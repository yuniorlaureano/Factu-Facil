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
    public class InvoiceController : BaseController
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Invoice>>> Get()
        {
            return Ok(await _invoiceService.GetAll(p => true, i => i.Client, i => i.InvoiceDetails, i => i.CreatedBy, i => i.UpdatedBy));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Invoice>> Get(Guid id)
        {
            try
            {
                return Ok(await _invoiceService.GetOne(p => p.Id == id, i => i.Client, i => i.InvoiceDetails, i => i.CreatedBy, i => i.UpdatedBy));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Invoice>> Create([FromBody]InvoiceViewModel model)
        {
            try
            {
                var invoice = new Invoice();
                invoice.Id = Guid.NewGuid();
                invoice.Number = model.Number;
                invoice.Total = model.Total;
                invoice.SubTotal = model.SubTotal;
                invoice.Isv = model.Isv;
                invoice.Disccount = model.Disccount;
                invoice.CreatedAt = DateTime.Now;
                invoice.CreatedById = GetUser().Id;
                invoice.UpdatedAt = DateTime.Now;
                invoice.UpdatedById = GetUser().Id;

                invoice.InvoiceDetails = model.InvoiceDetails.Select(x => new InvoiceDetail
                {
                    Id = Guid.NewGuid(),
                    Quantity = x.Quantity,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    InvoiceId = x.InvoiceId,
                    CreatedAt = DateTime.Now,
                    CreatedById = GetUser().Id,
                    UpdatedAt = DateTime.Now,
                    UpdatedById = GetUser().Id
                });               

                await _invoiceService.Add(invoice);
                return CreatedAtAction(nameof(Get), new { invoice.Id });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Invoice>> Update([FromBody]InvoiceViewModel model)
        {
            try
            {
                var invoice = await _invoiceService.GetOne(x => x.Id == model.Id, i => i.Client, i => i.InvoiceDetails);
                invoice.Number = model.Number;
                invoice.Total = model.Total;
                invoice.SubTotal = model.SubTotal;
                invoice.Isv = model.Isv;
                invoice.Disccount = model.Disccount;
                invoice.UpdatedAt = DateTime.Now;
                invoice.UpdatedById = GetUser().Id;

                List<InvoiceDetail> details = new List<InvoiceDetail>();
                foreach (var detail in model.InvoiceDetails)
                {
                    var existingDetail = invoice.InvoiceDetails.FirstOrDefault(x => x.Id == detail.Id);
                    if (existingDetail != null)
                    {
                        details.Add(new InvoiceDetail
                        {
                            Id = existingDetail.Id,
                            Quantity = detail.Quantity,
                            Price = detail.Price,
                            ProductId = detail.ProductId,
                            InvoiceId = detail.InvoiceId,
                            UpdatedAt = DateTime.Now,
                            UpdatedById = GetUser().Id
                        });
                    }
                    else
                    {
                        details.Add(new InvoiceDetail
                        {
                            Quantity = detail.Quantity,
                            Price = detail.Price,
                            ProductId = detail.ProductId,
                            InvoiceId = detail.InvoiceId,
                            CreatedAt = DateTime.Now,
                            CreatedById = GetUser().Id
                        });
                    }
                }

                invoice.InvoiceDetails = details;
                await _invoiceService.Update(invoice);
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
        public async Task<ActionResult<Invoice>> Delete(Guid id)
        {
            try
            {
                var entity = await _invoiceService.GetOne(p => p.Id == id);
                await _invoiceService.Delete(entity);
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
