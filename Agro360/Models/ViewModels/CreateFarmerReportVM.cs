using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agro360.Models.ViewModels
{
    public class CreateFarmerReportVM
    {
        public Report FarmerReport { get; set; }

        public IEnumerable<SelectListItem> HarvestSelectList { get; set; }

    }
}
