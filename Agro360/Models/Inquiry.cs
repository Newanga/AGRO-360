using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Agro360.Models
{
    public class Inquiry
    {
        [Key]
        public int InquiryId { get; set; }

        //FarmerId
        public string Id { get; set; }

        [ForeignKey("Id")]
        public virtual ApplicationUser ApplicationUser { get; set; }


        public int ReportId { get; set; }

        [ForeignKey("ReportId")]
        public virtual Report FarmerReport { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Please Enter a message")]
        public string Message { get; set; }

        public string InquiryState { get; set; }
    }
}
