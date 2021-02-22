using Agro360.Data;
using Agro360.Models;
using Agro360.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Agro360.Areas.Farmer.Controllers
{
    [Area("Farmer")]
    [Authorize(Roles = Roles.FarmerRole)]
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

        public IActionResult index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> New(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _db.FarmerReport.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }
            Inquiry newInquiry = new Inquiry();
            newInquiry.ReportId = report.ReportId;
            return View(newInquiry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewInquiry(Inquiry model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.InquiryState = InquiryState.Pending;
                model.Id= User.FindFirst(ClaimTypes.NameIdentifier).Value;
                _db.Inquiry.Add(model);
                await _db.SaveChangesAsync();
                return LocalRedirect("/Farmer/Home/Index");
            }
            else
            {
                return View(model);
            }

        }

        [HttpGet]
        public async Task<IActionResult> View(int? id)
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
                        InquiryId = Inquiry.InquiryId,
                        FarmerId=Inquiry.Id
                    }
                    ).Where(u => u.InquiryStatus == InquiryState.Pending && u.FarmerId== User.FindFirst(ClaimTypes.NameIdentifier).Value).ToList()
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
                        InquiryId = Inquiry.InquiryId,
                        FarmerId = Inquiry.Id
                    }
                    ).Where(u => u.InquiryStatus == InquiryState.Solved && u.FarmerId == User.FindFirst(ClaimTypes.NameIdentifier).Value).ToList()
            });
        }


    }
}
