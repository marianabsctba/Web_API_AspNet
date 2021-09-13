using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Donation.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DonationApiController : ControllerBase
    {
        private readonly IDonationService _donationService;

        public DonationApiController(
            IDonationService donationService)
        {
            _donationService = donationService;
        }

        [HttpGet("{orderAscendant:bool}/{search?}")]
        public async Task<ActionResult<IEnumerable<Domain.Model.Models.Donation>>> Get(
            bool orderAscendant,
            string search = null)
        {
            var donations = await _donationService
                .GetAllAsync(orderAscendant, search);

            return Ok(donations);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Domain.Model.Models.Donation>> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var donationModel = await _donationService.GetByIdAsync(id);

            if (donationModel == null)
            {
                return NotFound();
            }

            return Ok(donationModel);
        }

        [HttpPost]
        public async Task<ActionResult<Domain.Model.Models.Donation>> Post([FromBody] Domain.Model.Models.Donation donationModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(donationModel);
            }

            var donationCreated = await _donationService.CreateAsync(donationModel);

            return Ok(donationCreated);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Domain.Model.Models.Donation>> Put(int id, [FromBody] Domain.Model.Models.Donation donationModel)
        {
            if (id != donationModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(donationModel);
            }

            try
            {
                var donationEdited = await _donationService.EditAsync(donationModel);

                return Ok(donationEdited);
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

            await _donationService.DeleteAsync(id);

            return Ok();
        }

        [HttpGet("IsZipCodeValid/{zipCode}/{id}")]
        public async Task<IActionResult> IsZipCodeValid(string zipCode, int id)
        {
            var isValid = await _donationService.IsZipCodeValidAsync(zipCode, id);

            return Ok(isValid);
        }
    }
}
