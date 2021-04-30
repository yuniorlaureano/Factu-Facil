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
using System.Security.Authentication;

namespace FactuFacil.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return Ok(await _userService.GetAll(p => true));
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> Get(Guid id)
        {
            try
            {
                return Ok(await _userService.GetOne(p => true));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> Create([FromBody]UserViewModel model)
        {
            try
            {
                var user = new User();
                user.Id = Guid.NewGuid();
                user.Email = model.Email;
                user.Name = model.Name;
                user.Password = model.Password.HashPassword();

                await _userService.Add(user);
                return CreatedAtAction(nameof(Get), new { user.Id });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> Update([FromBody]UserViewModel model)
        {
            try
            {
                var user = await _userService.GetOne(p => p.Id == model.Id);
                user.Email = model.Email;
                user.Name = model.Name;

                await _userService.Update(user);
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

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> Delte(Guid id)
        {
            try
            {
                var entity = await _userService.GetOne(p => p.Id == id);
                await _userService.Delete(entity);
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


        [HttpPost("auth")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate([FromBody]AuthenticateRequest request)
        {
            try
            {
                var response = await _userService.Authenticate(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("logout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Logout()
        {
            try
            {
                HttpContext.Items.Remove("User");
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
