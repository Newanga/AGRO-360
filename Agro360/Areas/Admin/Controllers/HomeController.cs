using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Agro360.Models;
using Microsoft.AspNetCore.Authorization;
using Agro360.Utility;
using Agro360.Data;
using Microsoft.AspNetCore.Identity;
using Agro360.Models.ViewModels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace Agro360.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles=Roles.AdminRole)]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;


        public HomeController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View(LoadUpperDahsboardDataAsync());
        }

        private AdminDashBoardVM LoadUpperDahsboardDataAsync()
        {
            AdminDashBoardVM AdminDashboard = new AdminDashBoardVM();

            AdminDashboard.TotalAccounts = _db.ApplicationUser.ToListAsync().GetAwaiter().GetResult().Count;
            AdminDashboard.ActiveAccounts = _db.ApplicationUser.Where(u => u.LockoutEnd<=DateTime.Now).ToListAsync().GetAwaiter().GetResult().Count;
            AdminDashboard.DisabledAccounts = _db.ApplicationUser.Where(u => u.LockoutEnd >= DateTime.Now).ToListAsync().GetAwaiter().GetResult().Count;
            AdminDashboard.AdminCount= _userManager.GetUsersInRoleAsync(Roles.AdminRole).GetAwaiter().GetResult().Count;
            AdminDashboard.DOACount = _userManager.GetUsersInRoleAsync(Roles.DOARole).GetAwaiter().GetResult().Count;
            AdminDashboard.KeellsCount = _userManager.GetUsersInRoleAsync(Roles.KeellsRole).GetAwaiter().GetResult().Count;
            AdminDashboard.FarmerCount = _userManager.GetUsersInRoleAsync(Roles.FarmerRole).GetAwaiter().GetResult().Count;

            return AdminDashboard;

        }


    }
}
