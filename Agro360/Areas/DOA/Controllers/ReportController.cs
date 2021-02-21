using Agro360.Data;
using Agro360.Models;
using Agro360.Models.ViewModels;
using Agro360.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Agro360.Areas.DOA.Controllers
{
    [Area("DOA")]
    [Authorize(Roles = Roles.DOARole)]
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _db;


        public ReportController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAllPendingFarmerReports()
        {
           
            return Json(new
            {
                data = _db.FarmerReport
                .Join(
                    _db.HarvestType,
                    FarmerReport => FarmerReport.HarvestId,
                    HarvestType => HarvestType.HarvestId,
                    (FarmerReport, HarvestType) => new {
                        Id = FarmerReport.ReportId,
                        Quantity = FarmerReport.Quantity,
                        DateTime = FarmerReport.CreatedDate,
                        Date = FarmerReport.CreatedDate.ToString("dd/MM/yyyy"),
                        Time = FarmerReport.CreatedDate.ToShortTimeString(),
                        Harvest = HarvestType.HarvestType,
                        ReportStatus = FarmerReport.ReportState
                    }
                    ).Where(u => u.ReportStatus == ReportState.Farmer_Reviewable)
                    .Where(u => u.DateTime.AddDays(1) < DateTime.Now).ToList()
            });
        }

        [HttpGet]
        public IActionResult GetAllApprovedFarmerReports()
        {

            return Json(new
            {
                data = _db.FarmerReport
                .Join(
                    _db.HarvestType,
                    FarmerReport => FarmerReport.HarvestId,
                    HarvestType => HarvestType.HarvestId,
                    (FarmerReport, HarvestType) => new {
                        Id = FarmerReport.ReportId,
                        Quantity = FarmerReport.Quantity,
                        DateTime = FarmerReport.CreatedDate,
                        Date = FarmerReport.CreatedDate.ToString("dd/MM/yyyy"),
                        Time = FarmerReport.CreatedDate.ToShortTimeString(),
                        Harvest = HarvestType.HarvestType,
                        ReportStatus = FarmerReport.ReportState
                    }
                    ).Where(u => u.ReportStatus == ReportState.DOA_Approved|| u.ReportStatus ==ReportState.Keells_Approved|| u.ReportStatus==ReportState.Keells_Rejected).ToList()
            });
        }

        [HttpGet]
        public IActionResult GetAllRejectedFarmerReports()
        {

            return Json(new
            {
                data = _db.FarmerReport
                .Join(
                    _db.HarvestType,
                    FarmerReport => FarmerReport.HarvestId,
                    HarvestType => HarvestType.HarvestId,
                    (FarmerReport, HarvestType) => new {
                        Id = FarmerReport.ReportId,
                        Quantity = FarmerReport.Quantity,
                        DateTime = FarmerReport.CreatedDate,
                        Date = FarmerReport.CreatedDate.ToString("dd/MM/yyyy"),
                        Time = FarmerReport.CreatedDate.ToShortTimeString(),
                        Harvest = HarvestType.HarvestType,
                        ReportStatus = FarmerReport.ReportState
                    }
                    ).Where(u => u.ReportStatus == ReportState.DOA_Rejected).ToList()
            });
        }

        [HttpGet]
        public async Task<IActionResult> ViewPending(int? id)
        {   
            
            FarmerReportVM reportVM= new FarmerReportVM();
          
            if (id != null)
            {
                var report= await _db.FarmerReport.FindAsync(id);
                if (report != null)
                {
                    var harvest = await _db.HarvestType.FindAsync(report.HarvestId);
                    reportVM.ReportId = report.ReportId;
                    reportVM.Quantity = report.Quantity;
                    reportVM.CreatedDate = report.CreatedDate.ToString("dd/MM/yyyy");
                    reportVM.CreatedTime = report.CreatedDate.ToShortTimeString();
                    reportVM.HarvestType = harvest.HarvestType;
                    reportVM.ImageLinks = await Blob.GetAllContainersImages("0000"+ report.ReportId.ToString());
                }
                else
                {
                    return NotFound();
                }

            }
            else
            {
                return NotFound();
            }
            return View(reportVM);
        }

        [HttpGet]
        public async Task<IActionResult> View(int? id)
        {

            FarmerReportVM reportVM = new FarmerReportVM();

            if (id != null)
            {
                var report = await _db.FarmerReport.FindAsync(id);
                if (report != null)
                {
                    var harvest = await _db.HarvestType.FindAsync(report.HarvestId);
                    reportVM.ReportId = report.ReportId;
                    reportVM.Quantity = report.Quantity;
                    reportVM.CreatedDate = report.CreatedDate.ToString("dd/MM/yyyy");
                    reportVM.CreatedTime = report.CreatedDate.ToShortTimeString();
                    reportVM.HarvestType = harvest.HarvestType;
                    reportVM.ImageLinks = await Blob.GetAllContainersImages("0000" + report.ReportId.ToString());
                }
                else
                {
                    return NotFound();
                }

            }
            else
            {
                return NotFound();
            }
            return View(reportVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectReport(int? reportId)
        {
            if (reportId == null)
            {
                return NotFound();
            }
            var report = await _db.FarmerReport.FindAsync(reportId);
            if (report == null)
            {
                return NotFound();
            }

            Report farmerReport=await _db.FarmerReport.FindAsync(reportId);
            farmerReport.ReportState = ReportState.DOA_Rejected;
            _db.FarmerReport.Update(farmerReport);
            await _db.SaveChangesAsync();
            return LocalRedirect("/DOA/Home/Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptReport(int? reportId)
        {
            if (reportId == null)
            {
                return NotFound();
            }
            var report = await _db.FarmerReport.FindAsync(reportId);
            if (report == null)
            {
                return NotFound();
            }

            Report farmerReport = await _db.FarmerReport.FindAsync(reportId);
            farmerReport.ReportState = ReportState.DOA_Approved;
            _db.FarmerReport.Update(farmerReport);
            await _db.SaveChangesAsync();
            return LocalRedirect("/DOA/Home/Index");

        }
    }
}
