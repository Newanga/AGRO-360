using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Agro360.Models.ViewModels
{
    public class FarmerReportVM
    {
        public int ReportId { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }


        public string Address { get; set; }

        public string NIC { get; set; }

        [Display(Name = "Contact No")]
        public string ContactNo { get; set; }

        [Display(Name = "Report Date")]
        public string CreatedDate { get; set; }

        [Display(Name = "Report Time")]
        public string CreatedTime { get; set; }

        [Display(Name= "Harvest Type")]
        public string HarvestType { get; set; }

        [Required(ErrorMessage ="Please Enter new Quantity")]
        public int Quantity { get; set; }

        public List<Uri> ImageLinks { get; set; }
    }
}
