using Agro360.Data;
using Agro360.Models;
using Agro360.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Agro360.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.AdminRole)]
    public class FarmerController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;


        public FarmerController(ApplicationDbContext db,
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
        public async Task<IActionResult> Get()
        {
            return Json(new { data = await _userManager.GetUsersInRoleAsync(Roles.FarmerRole) });
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
