using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationDonation.Models;
using WebApplicationDonation.Services;

namespace WebApplicationDonation.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserHttpService _userHttpService;

        public UserController(
            IUserHttpService userHttpService)
        {
            _userHttpService = userHttpService;
        }

        // GET: User
        public async Task<IActionResult> Index(UserIndexViewModel userIndexRequest)
        {
            var userIndexViewModel = new UserIndexViewModel
            {
                Search = userIndexRequest.Search,
                OrderAscendant = userIndexRequest.OrderAscendant,
                Users = await _userHttpService.GetAllAsync(
                    userIndexRequest.OrderAscendant,
                    userIndexRequest.Search)
            };

            return View(userIndexViewModel);
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userViewModel = await _userHttpService.GetByIdAsync(id.Value);

            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }

            var userCreated = await _userHttpService.CreateAsync(userViewModel);

            return RedirectToAction(nameof(Details), new { id = userCreated.Id });
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userViewModel = await _userHttpService.GetByIdAsync(id.Value);
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserViewModel userViewModel)
        {
            if (id != userViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }

            try
            {
                await _userHttpService.EditAsync(userViewModel);
            }
            catch (DbUpdateConcurrencyException) 
            {
                var exists = await UserExistsAsync(userViewModel.Id);
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

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userViewModel = await _userHttpService.GetByIdAsync(id.Value);
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userHttpService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> UserExistsAsync(int id)
        {
            var user = await _userHttpService.GetByIdAsync(id);

            var any = user != null;

            return any;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsCpfValid(string cpf, int id)
        {
            return await _userHttpService.IsCpfValidAsync(cpf, id)
                ? Json(true)
                : Json($"O CPF {cpf} já está sendo usado.");
        }
    }
}
