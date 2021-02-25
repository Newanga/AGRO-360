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

namespace Agro360.Areas.Keells.Controllers
{
    [Area("Keells")]
    [Authorize(Roles = Roles.KeellsRole)]
    public class InquiryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;


        public InquiryController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllPendingInquiry()
        {

            return Json(new
            {
                data = _db.Inquiry
                .Join(
                    _db.FarmerReport,
                    Inquiry => Inquiry.ReportId,
                    FarmerReport => FarmerReport.ReportId,
                    (Inquiry, FarmerReport) => new
                    {
                        ReportId = FarmerReport.ReportId,
                        Date = Inquiry.CreatedDate.ToString("dd/MM/yyyy"),
                        Time = Inquiry.CreatedDate.ToShortTimeString(),
                        InquiryStatus = Inquiry.InquiryState,
                        InquiryId=Inquiry.InquiryId
                    }
                    ).Where(u => u.InquiryStatus == InquiryState.Pending).ToList()
            });
        }

        [HttpGet]
        public IActionResult GetAllSolvedInquiry()
        {

            return Json(new
            {
                data = _db.Inquiry
                .Join(
                    _db.FarmerReport,
                    Inquiry => Inquiry.ReportId,
                    FarmerReport => FarmerReport.ReportId,
                    (Inquiry, FarmerReport) => new
                    {
                        ReportId = FarmerReport.ReportId,
                        Date = Inquiry.CreatedDate.ToString("dd/MM/yyyy"),
                        Time = Inquiry.CreatedDate.ToShortTimeString(),
                        InquiryStatus = Inquiry.InquiryState,
                        InquiryId = Inquiry.InquiryId
                    }
                    ).Where(u => u.InquiryStatus == InquiryState.Solved).ToList()
            });
        }

        [HttpGet]
        public async Task<IActionResult> ViewPending(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Inquiry inquiry = await _db.Inquiry.FindAsync(id);
            if (inquiry == null)
            {
                return NotFound();
            }
            return View(inquiry);
        }

        [HttpGet]
        public async Task<IActionResult> ViewSolved(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Inquiry inquiry = await _db.Inquiry.FindAsync(id);
            if (inquiry == null)
            {
                return NotFound();
            }
            return View(inquiry);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SolveInquiry(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var inquiry = await _db.Inquiry.FindAsync(id);
            if (inquiry == null)
            {
                return NotFound();
            }

            inquiry.InquiryState = InquiryState.Solved;
            _db.Inquiry.Update(inquiry);
            await _db.SaveChangesAsync();
            return LocalRedirect("/Keells/Inquiry/Index");

        }

    }
}
