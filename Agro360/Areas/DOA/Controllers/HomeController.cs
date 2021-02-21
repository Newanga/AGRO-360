using Agro360.Data;
using Agro360.Models.ViewModels;
using Agro360.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;


namespace Agro360.Areas.DOA.Controllers
{
    [Area("DOA")]
    [Authorize(Roles = Roles.DOARole)]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;


        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View(LoadUpperDahsboardData());
        }

        private DOADashBoardVM LoadUpperDahsboardData()
        {
            DOADashBoardVM doaDashBoardVM = new DOADashBoardVM();

            
            doaDashBoardVM.PendingReportCount = _db.FarmerReport
                .Join(
                    _db.HarvestType,
                    FarmerReport => FarmerReport.HarvestId,
                    HarvestType => HarvestType.HarvestId,
                    (FarmerReport, HarvestType) => new {
                        Id = FarmerReport.ReportId,
                        DateTime = FarmerReport.CreatedDate,
                        ReportStatus = FarmerReport.ReportState
                    }
                    ).Where(u => u.ReportStatus == ReportState.Farmer_Reviewable)
                    .Where(u => u.DateTime.AddDays(1) < DateTime.Now).ToList().Count();


            doaDashBoardVM.ApprovedReportCount = _db.FarmerReport
                .Join(
                    _db.HarvestType,
                    FarmerReport => FarmerReport.HarvestId,
                    HarvestType => HarvestType.HarvestId,
                    (FarmerReport, HarvestType) => new {
                        Id = FarmerReport.ReportId,
                        DateTime = FarmerReport.CreatedDate,
                        ReportStatus = FarmerReport.ReportState
                    }
                    ).Where(u => u.ReportStatus == ReportState.DOA_Approved || u.ReportStatus == ReportState.Keells_Approved || u.ReportStatus == ReportState.Keells_Rejected).ToList().Count();

            doaDashBoardVM.RejectedReportCount = _db.FarmerReport
                .Join(
                    _db.HarvestType,
                    FarmerReport => FarmerReport.HarvestId,
                    HarvestType => HarvestType.HarvestId,
                    (FarmerReport, HarvestType) => new
                    {
                        Id = FarmerReport.ReportId,
                        ReportStatus = FarmerReport.ReportState
                    }
                    ).Where(u => u.ReportStatus == ReportState.DOA_Rejected).ToList().Count();

            return doaDashBoardVM;
        }
    }
}
