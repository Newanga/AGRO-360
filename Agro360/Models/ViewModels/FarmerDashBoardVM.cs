using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agro360.Models.ViewModels
{
    public class FarmerDashBoardVM
    {
        public int ReviewableReportCount { get; set; }

        public int PendingReportCount { get; set; }

        public int ApprovedReportCount { get; set; }

        public int RejectedReportCount { get; set; }

    }
}
