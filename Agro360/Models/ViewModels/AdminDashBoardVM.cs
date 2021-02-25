using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agro360.Models.ViewModels
{
    public class AdminDashBoardVM
    {
        public int TotalAccounts { get; set; }

        public int ActiveAccounts { get; set; }

        public int DisabledAccounts { get; set; }

        public int AdminCount { get; set; }

        public int DOACount { get; set; }

        public int KeellsCount { get; set; }

        public int FarmerCount { get; set; }



    }
}
