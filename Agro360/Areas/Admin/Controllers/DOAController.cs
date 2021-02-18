using Agro360.Data;
using Agro360.Models;
using Agro360.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agro360.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.AdminRole)]
    public class DOAController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public DOAController(ApplicationDbContext db,
           UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { Data = await _userManager.GetUsersInRoleAsync(Roles.DOARole) });
        }

        [HttpGet]
        //Create New DOA Staff
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        //Submit New DOA Staff Data
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = model.mail,
                    UserName = model.uname,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    NIC = model.NIC,
                    Address = model.Address,
                    ZipCode = model.ZipCode,
                    PhoneNumber = model.ContactNo

                };
                var result = await _userManager.CreateAsync(user, model.Psswd);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.DOARole);
                    return LocalRedirect("/Admin/DOA/Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LockUnlock([FromBody] string id)
        {
            var User = _db.ApplicationUser.Where(u => u.Id == id).FirstOrDefault();
            if (User == null)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }
            if (User.LockoutEnd != null && User.LockoutEnd > DateTime.Now)
            {
                User.LockoutEnd = DateTime.Now;
            }
            else
            {
                User.LockoutEnd = DateTime.Now.AddYears(100);
            }
            _db.ApplicationUser.Update(User);
            await _db.SaveChangesAsync();

            return Json(new { success = true, message = "Operation Successful." });
        }
    }
}
