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

namespace Agro360.Areas.Farmer.Controllers
{
    [Area("Farmer")]
    [Authorize(Roles = Roles.FarmerRole)]
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;


        public ReportController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult New()
        {
            Blob blob = new Blob();
            CreateFarmerReportVM reportVM = new CreateFarmerReportVM()
            {
                FarmerReport = new Report(),
                HarvestSelectList = _db.HarvestType.Select(i => new SelectListItem
                {
                    Text=i.HarvestType,
                    Value=i.HarvestId.ToString()
                })
            };
            return View(reportVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewReport(CreateFarmerReportVM model)
        {
            if (ModelState.IsValid==true)
            {
                Report report = new Report();
                report.Quantity = model.FarmerReport.Quantity;
                report.HarvestId = model.FarmerReport.HarvestId;
                report.CreatedDate = DateTime.Now;
                report.ReportState = ReportState.Farmer_Reviewable;
                //Gets current logged in farmer User ID
                report.Id = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                _db.FarmerReport.Add(report);
                await _db.SaveChangesAsync();
                //Get newly created farmer report primary key
                int id = report.ReportId;
                //Upload images to blob container
                await Blob.UploadImageAsync(model.FarmerReport.HarvestImages, "0000" + id.ToString());
                return LocalRedirect("/Farmer/Home/Index");
            }
            return View(model);

        }

        [HttpGet]
        public IActionResult GetAllReviewableFarmerReports()
        {
            var CurrentFarmerId= this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Json(new
            {
                data = _db.FarmerReport
                .Join(
                    _db.HarvestType,
                    FarmerReport => FarmerReport.HarvestId,
                    HarvestType => HarvestType.HarvestId,
                    (FarmerReport, HarvestType) => new {
                                Id = FarmerReport.ReportId,
                                FarmerId=FarmerReport.Id,
                                Quantity=FarmerReport.Quantity,
                                DateTime = FarmerReport.CreatedDate,
                                Date=FarmerReport.CreatedDate.ToString("dd/MM/yyyy"),
                                Time =FarmerReport.CreatedDate.ToShortTimeString(),
                                Harvest = HarvestType.HarvestType,
                                ReportStatus=FarmerReport.ReportState
                            }
                    ).Where(u=>u.ReportStatus==ReportState.Farmer_Reviewable)
                    .Where(u=>u.FarmerId==CurrentFarmerId)
                    .Where(u=>u.DateTime.AddDays(1)>DateTime.Now).ToList()
            });
        }


        [HttpGet]
        public IActionResult GetAllPendingFarmerReports()
        {
            var CurrentFarmerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Json(new
            {
                data = _db.FarmerReport
                .Join(
                    _db.HarvestType,
                    FarmerReport => FarmerReport.HarvestId,
                    HarvestType => HarvestType.HarvestId,
                    (FarmerReport, HarvestType) => new {
                        Id = FarmerReport.ReportId,
                        FarmerId = FarmerReport.Id,
                        Quantity = FarmerReport.Quantity,
                        DateTime = FarmerReport.CreatedDate,
                        Date = FarmerReport.CreatedDate.ToString("dd/MM/yyyy"),
                        Time = FarmerReport.CreatedDate.ToShortTimeString(),
                        Harvest = HarvestType.HarvestType,
                        ReportStatus = FarmerReport.ReportState
                    }
                    ).Where(u => u.ReportStatus == ReportState.Farmer_Reviewable)
                    .Where(u => u.FarmerId == CurrentFarmerId)
                    .Where(u => u.DateTime.AddDays(1) < DateTime.Now).ToList()
            });
        }


        [HttpGet]
        public IActionResult GetAllApprovedFarmerReports()
        {
            var CurrentFarmerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Json(new
            {
                data = _db.FarmerReport
                .Join(
                    _db.HarvestType,
                    FarmerReport => FarmerReport.HarvestId,
                    HarvestType => HarvestType.HarvestId,
                    (FarmerReport, HarvestType) => new {
                        Id = FarmerReport.ReportId,
                        FarmerId = FarmerReport.Id,
                        Quantity = FarmerReport.Quantity,
                        DateTime = FarmerReport.CreatedDate,
                        Date = FarmerReport.CreatedDate.ToString("dd/MM/yyyy"),
                        Time = FarmerReport.CreatedDate.ToShortTimeString(),
                        Harvest = HarvestType.HarvestType,
                        ReportStatus = FarmerReport.ReportState
                    }
                    ).Where(u => u.ReportStatus == ReportState.DOA_Approved|| u.ReportStatus == ReportState.Keells_Approved)
                    .Where(u => u.FarmerId == CurrentFarmerId).ToList()
            });
        }

        [HttpGet]
        public IActionResult GetAllRejectedFarmerReports()
        {
            var CurrentFarmerId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Json(new
            {
                data = _db.FarmerReport
                .Join(
                    _db.HarvestType,
                    FarmerReport => FarmerReport.HarvestId,
                    HarvestType => HarvestType.HarvestId,
                    (FarmerReport, HarvestType) => new {
                        Id = FarmerReport.ReportId,
                        FarmerId = FarmerReport.Id,
                        Quantity = FarmerReport.Quantity,
                        DateTime = FarmerReport.CreatedDate,
                        Date = FarmerReport.CreatedDate.ToString("dd/MM/yyyy"),
                        Time = FarmerReport.CreatedDate.ToShortTimeString(),
                        Harvest = HarvestType.HarvestType,
                        ReportStatus = FarmerReport.ReportState
                    }
                    ).Where(u => u.ReportStatus == ReportState.DOA_Rejected || u.ReportStatus == ReportState.Keells_Rejected)
                    .Where(u => u.FarmerId == CurrentFarmerId).ToList()
            });
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            FarmerReportVM reportVM = new FarmerReportVM();

            if (id != null)
            {
                var report = await _db.FarmerReport.FindAsync(id);
                var currentLoggedUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (report != null && 
                    report.Id == currentLoggedUserId && 
                    report.ReportState==ReportState.Farmer_Reviewable 
                    && report.CreatedDate.AddDays(1) > DateTime.Now)
                {
                    var harvest = await _db.HarvestType.FindAsync(report.HarvestId);
                    reportVM.ReportId = report.ReportId;
                    reportVM.Quantity = report.Quantity;
                    reportVM.CreatedDate = report.CreatedDate.ToString("dd/MM/yyyy");
                    reportVM.CreatedTime = report.CreatedDate.ToShortTimeString();
                    reportVM.HarvestType = harvest.HarvestType;
                    reportVM.ImageLinks = await Blob.GetAllContainersImages("0000" +report.ReportId.ToString());
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
        public async Task<IActionResult> UpdateReport(FarmerReportVM model)
        {
            if (ModelState.IsValid == true)
            {
                Report report = new Report();
                report = await _db.FarmerReport.FindAsync(model.ReportId);
                report.Quantity = model.Quantity;
                _db.FarmerReport.Update(report);
                await _db.SaveChangesAsync();
                return LocalRedirect("/Farmer/Home/Index");
            }
            return View(model);

        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            FarmerReportVM reportVM = new FarmerReportVM();

            if (id != null)
            {
                var report = await _db.FarmerReport.FindAsync(id);
                var currentLoggedUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (report != null &&
                    report.Id == currentLoggedUserId &&
                    report.ReportState == ReportState.Farmer_Reviewable
                    && report.CreatedDate.AddDays(1) > DateTime.Now)
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
        public async Task<IActionResult> DeleteReport(int? reportId)
        {
            if (reportId == null)
            {
                return NotFound();
            }
            var report =await _db.FarmerReport.FindAsync(reportId);
            if (report == null)
            {
                return NotFound();
            }

            _db.FarmerReport.Remove(report);
            await _db.SaveChangesAsync();
            await Blob.DeleteContainerAsync("0000" + reportId.ToString());
            return LocalRedirect("/Farmer/Home/Index");

        }
        [HttpGet]
        public async Task<IActionResult> View(int? id)
        {   
            
            FarmerReportVM reportVM= new FarmerReportVM();
          
            if (id != null)
            {
                var report= await _db.FarmerReport.FindAsync(id);
                var currentLoggedUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (report != null && report.Id==currentLoggedUserId)
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
    }
}
