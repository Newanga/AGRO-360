using Agro360.Data;
using Agro360.Models;
using Agro360.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agro360.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.AdminRole)]
    public class HarvestController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HarvestController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var harvest = _db.HarvestType.FindAsync(id).GetAwaiter().GetResult();

            if (harvest == null)
            {
                return NotFound();
            }
            else
            {
                return View(harvest);
            }

        }

        [HttpPost]
        public async Task<IActionResult> UpdateHarvest(Harvest model)
        {
            if (ModelState.IsValid)
            {
                _db.HarvestType.Update(model);
                await _db.SaveChangesAsync();
                return LocalRedirect("/Admin/Harvest/Index");
            }
            else
            {
                return View(model);
            }

        }


        [HttpPost]
        //Submit New Keels Staff Data
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewHarvest(Harvest model)
        {
            if (ModelState.IsValid)
            {
                var newHarvest = new Harvest
                {
                    HarvestType = model.HarvestType

                };

                _db.HarvestType.Add(newHarvest);
                await _db.SaveChangesAsync();
                return LocalRedirect("/Admin/Harvest/Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult GetAllHarvestTypes()
        {
            return Json(new
            {
                data=_db.HarvestType.ToList()
            });
        }

    }
}
