using Agro360.Data;
using Agro360.Models.ViewModels;
using Agro360.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;


namespace Agro360.Areas.Keells.Controllers
{
    [Area("Keells")]
    [Authorize(Roles = Roles.KeellsRole)]
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

        private KeeelsDashBoardVM LoadUpperDahsboardData()
        {
            KeeelsDashBoardVM keeelsDashBoardVM = new KeeelsDashBoardVM();

            keeelsDashBoardVM.PendingReportCount = _db.FarmerReport
                .Join(
                    _db.HarvestType,
                    FarmerReport => FarmerReport.HarvestId,
                    HarvestType => HarvestType.HarvestId,
                    (FarmerReport, HarvestType) => new {
                        Id = FarmerReport.ReportId,
                        ReportStatus = FarmerReport.ReportState
                    }
                    ).Where(u => u.ReportStatus == ReportState.DOA_Approved).ToList().Count();


            keeelsDashBoardVM.ApprovedReportCount = _db.FarmerReport
                .Join(
                    _db.HarvestType,
                    FarmerReport => FarmerReport.HarvestId,
                    HarvestType => HarvestType.HarvestId,
                    (FarmerReport, HarvestType) => new
                    {
                        Id = FarmerReport.ReportId,
                        ReportStatus = FarmerReport.ReportState
                    }
                    ).Where(u => u.ReportStatus == ReportState.Keells_Approved).ToList().Count();

            keeelsDashBoardVM.RejectedReportCount = _db.FarmerReport
                .Join(
                    _db.HarvestType,
                    FarmerReport => FarmerReport.HarvestId,
                    HarvestType => HarvestType.HarvestId,
                    (FarmerReport, HarvestType) => new
                    {
                        Id = FarmerReport.ReportId,
                        ReportStatus = FarmerReport.ReportState
                    }
                    ).Where(u => u.ReportStatus == ReportState.Keells_Rejected).ToList().Count();

            return keeelsDashBoardVM;
        }
    }
}
