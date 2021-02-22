using Agro360.Data;
using Agro360.Models;
using Agro360.Models.ViewModels;
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

            return View(LoadUpperDahsboardData());
        }

        private FarmerDashBoardVM LoadUpperDahsboardData()
        {
            var CurrentFarmerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            FarmerDashBoardVM farmerDashboard = new FarmerDashBoardVM();
            farmerDashboard.ReviewableReportCount = _db.FarmerReport
                 .Join(
                     _db.HarvestType,
                     FarmerReport => FarmerReport.HarvestId,
                     HarvestType => HarvestType.HarvestId,
                     (FarmerReport, HarvestType) => new
                     {
                         Id = FarmerReport.ReportId,
                         FarmerId = FarmerReport.Id,
                         DateTime = FarmerReport.CreatedDate,
                         ReportStatus = FarmerReport.ReportState
                     }
                     ).Where(u => u.ReportStatus == ReportState.Farmer_Reviewable)
                     .Where(u => u.FarmerId == CurrentFarmerId)
                     .Where(u => u.DateTime.AddDays(1) > DateTime.Now).ToList().Count;

            farmerDashboard.PendingReportCount = _db.FarmerReport
                .Join(
                    _db.HarvestType,
                    FarmerReport => FarmerReport.HarvestId,
                    HarvestType => HarvestType.HarvestId,
                    (FarmerReport, HarvestType) => new
                    {
                        Id = FarmerReport.ReportId,
                        FarmerId = FarmerReport.Id,
                        DateTime = FarmerReport.CreatedDate,
                        ReportStatus = FarmerReport.ReportState
                    }
                    ).Where(u => u.ReportStatus == ReportState.Farmer_Reviewable)
                    .Where(u => u.FarmerId == CurrentFarmerId)
                    .Where(u => u.DateTime.AddDays(1) < DateTime.Now).ToList().Count();


            farmerDashboard.ApprovedReportCount = _db.FarmerReport
                .Join(
                    _db.HarvestType,
                    FarmerReport => FarmerReport.HarvestId,
                    HarvestType => HarvestType.HarvestId,
                    (FarmerReport, HarvestType) => new
                    {
                        Id = FarmerReport.ReportId,
                        FarmerId = FarmerReport.Id,
                        ReportStatus = FarmerReport.ReportState
                    }
                    ).Where(u => u.ReportStatus == ReportState.DOA_Approved || u.ReportStatus == ReportState.Keells_Approved)
                    .Where(u => u.FarmerId == CurrentFarmerId).ToList().Count();


            farmerDashboard.RejectedReportCount = _db.FarmerReport
                .Join(
                    _db.HarvestType,
                    FarmerReport => FarmerReport.HarvestId,
                    HarvestType => HarvestType.HarvestId,
                    (FarmerReport, HarvestType) => new
                    {
                        Id = FarmerReport.ReportId,
                        FarmerId = FarmerReport.Id,
                        ReportStatus = FarmerReport.ReportState
                    }
                    ).Where(u => u.ReportStatus == ReportState.DOA_Rejected || u.ReportStatus == ReportState.Keells_Rejected)
                    .Where(u => u.FarmerId == CurrentFarmerId).ToList().Count();

            return farmerDashboard;
        }
    }
}
