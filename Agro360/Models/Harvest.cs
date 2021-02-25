using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agro360.Models

{
    public class Harvest
    {
        [Key]
        public int HarvestId { get; set; }

        [DisplayName("Harvest Type")]
        [Required]
        public string HarvestType { get; set; }
    }
}
