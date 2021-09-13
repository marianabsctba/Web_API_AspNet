using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationDonation.Models;
using WebApplicationDonation.Services;

namespace WebApplicationDonation.Controllers

{
    [Authorize]
    public class DonationController : Controller

    {
        private readonly IDonationHttpService _donationHttpService;
        private readonly IUserHttpService _userHttpService;

        public DonationController(
            IDonationHttpService donationHttpService,
            IUserHttpService userHttpService)
        {
            _donationHttpService = donationHttpService;
            _userHttpService = userHttpService;
        }

        // GET: Donation
        public async Task<IActionResult> Index(
            DonationIndexViewModel donationIndexRequest)
        {
            var donationIndexViewModel = new DonationIndexViewModel
            {
                Search = donationIndexRequest.Search,
                OrderAscendant = donationIndexRequest.OrderAscendant,
                Donations = await _donationHttpService.GetAllAsync(
                    donationIndexRequest.OrderAscendant,
                    donationIndexRequest.Search)
            };
            return View(donationIndexViewModel);
        }

        // GET: Donation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donationViewModel = await _donationHttpService.GetByIdAsync(id.Value);

            if (donationViewModel == null)
            {
                return NotFound();
            }

            return View(donationViewModel);
        }

        // GET: Donation/Create
        public async Task<IActionResult> Create()
        {
            await SelectUsers();

            return View();
        }

        // POST: Donation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DonationViewModel donationViewModel)
        {
            if (!ModelState.IsValid)
            {
                await SelectUsers(donationViewModel.UserId);
                return View(donationViewModel);
            }

            var donationCreated = await _donationHttpService.CreateAsync(donationViewModel);

            return RedirectToAction(nameof(Details), new { id = donationCreated.Id });
        }

        private async Task SelectUsers(int? userId = null)
        {
            var users = await _userHttpService.GetAllAsync(true);

            ViewBag.Users = new SelectList(
                users,
                nameof(UserViewModel.Id),
                nameof(UserViewModel.FullName),
                userId);
        }

        // GET: Donation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donationViewModel = await _donationHttpService.GetByIdAsync(id.Value);
            if (donationViewModel == null)
            {
                return NotFound();
            }

            await SelectUsers(donationViewModel.Id);

            return View(donationViewModel);
        }

        // POST: Donation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DonationViewModel donationViewModel)
        {
            if (id != donationViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await SelectUsers(donationViewModel.UserId);

                return View(donationViewModel);
            }

            try
            {
                await _donationHttpService.EditAsync(donationViewModel);
            }
            catch (DbUpdateConcurrencyException) 
            {
                var exists = await DonationViewModelExistsAsync(donationViewModel.Id);
                if (!exists)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Donation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donationViewModel = await _donationHttpService.GetByIdAsync(id.Value);
            if (donationViewModel == null)
            {
                return NotFound();
            }

            return View(donationViewModel);
        }

        // POST: Donation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _donationHttpService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DonationViewModelExistsAsync(int id)
        {
            var donation = await _donationHttpService.GetByIdAsync(id);

            var any = donation != null;

            return any;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsZipCodeValid(string donationZipCode, int id)
        {
            return await _donationHttpService.IsZipCodeValidAsync(donationZipCode, id)
                ? Json(true)
                : Json($"O CEP {donationZipCode} já está sendo usado.");
        }
    }
}

