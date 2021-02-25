using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agro360.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        //Farmer ID from identity table of database
        public string Id { get; set; }

        [ForeignKey("Id")]
        public virtual ApplicationUser ApplicationUser { get; set; }


        public DateTime CreatedDate { get; set; }

        [Display(Name ="Report State")]
        public string ReportState { get; set; }


        [Required]
        [Range(20, 1000,ErrorMessage ="Quantity should be in range 20-1000")]
        public int Quantity { get; set; }

        [DisplayName("Harvest Type")]
        [Required(ErrorMessage ="Please select a Harvest Type")]
        public int HarvestId { get; set; }


        [ForeignKey("HarvestId")]
        public virtual Harvest Harvest { get; set; }



        [Required(ErrorMessage ="Please Upload Harvest Images")]
        [DisplayName("Harvest Images")]
        [NotMapped]
        public IEnumerable<IFormFile> HarvestImages { get; set; }



    }

}
