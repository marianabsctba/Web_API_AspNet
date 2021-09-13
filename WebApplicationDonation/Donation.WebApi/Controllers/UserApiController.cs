using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Donation.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserApiController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserApiController(
            IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{orderAscendant" +
                 ":bool}/{search?}")]
        public async Task<ActionResult<IEnumerable<User>>> Get(
            bool orderAscendant,
            string search = null)
        {
            var users = await _userService.GetAllAsync(orderAscendant, search);

            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var userModel = await _userService.GetByIdAsync(id);

            if (userModel == null)
            {
                return NotFound();
            }

            return Ok(userModel);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(userModel);
            }

            var userCreated = await _userService.CreateAsync(userModel);

            return Ok(userCreated);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<User>> Put(int id, [FromBody] User userModel)
        {
            if (id != userModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(userModel);
            }

            try
            {
                var userEdited = await _userService.EditAsync(userModel);

                return Ok(userEdited);
            }
            catch (DbUpdateConcurrencyException)
            {

                return StatusCode(409);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            await _userService.DeleteAsync(id);

            return Ok();
        }

        [HttpGet("IsCpfValid/{cpf}/{id}")]
        public async Task<IActionResult> IsCpfValid(string cpf, int id)
        {
            var isValid = await _userService.IsCpfValidAsync(cpf, id);

            return Ok(isValid);
        }
    }
}
