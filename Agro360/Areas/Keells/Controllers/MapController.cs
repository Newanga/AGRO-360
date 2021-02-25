using Agro360.Data;
using Agro360.Models;
using Agro360.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;

namespace Agro360.Areas.Keells.Controllers
{
    [Area("Keells")]
    [Authorize(Roles = Roles.KeellsRole)]
    public class MapController : Controller
    {


        private readonly ApplicationDbContext _db;

        public MapController(ApplicationDbContext db)
        {
            _db = db;
        }



        public IActionResult GetMapData()
        {
            var data = _db.FarmerReport
                .Join(
                    _db.HarvestType,
                    FarmerReport => FarmerReport.HarvestId,
                    HarvestType => HarvestType.HarvestId,
                    (FarmerReport, HarvestType) => new { FarmerReport, HarvestType })

                .Join(_db.ApplicationUser,
                        report => report.FarmerReport.Id,
                        Farmer => Farmer.Id,
                        (report, Farmer) => new { report, Farmer })

                    .Select(x => new
                    {
                        FName = x.Farmer.FirstName,
                        LastName = x.Farmer.LastName,
                        address=x.Farmer.Address,
                        contactNo=x.Farmer.PhoneNumber,
                        lng = float.Parse(x.Farmer.Longitude),
                        lat = float.Parse(x.Farmer.Latitude),
                        HarvestType = x.report.HarvestType.HarvestType,
                        Quantity = x.report.FarmerReport.Quantity,
                        HarvestDate = x.report.FarmerReport.CreatedDate.ToString("dd/MM/yyyy"),
                        ReportState = x.report.FarmerReport.ReportState,
                        ReportDate = x.report.FarmerReport.CreatedDate
                    }
                    ).Where(u =>
                    (u.ReportState == ReportState.Farmer_Reviewable && u.ReportDate.AddDays(1) < DateTime.Now) ||
                                   u.ReportState==ReportState.DOA_Approved||
                                   u.ReportState == ReportState.Keells_Approved ||
                                   u.ReportState == ReportState.DOA_Rejected ||
                                   u.ReportState == ReportState.Keells_Rejected).ToList();


            return Json(data);

        }
    }
}
