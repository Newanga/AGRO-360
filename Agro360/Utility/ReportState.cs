using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agro360.Utility
{
    //These constants represent various states a report submitted by a farmer could be before and after approval
    public static class ReportState
    {
        //Farmer report states
        public const string Farmer_Reviewable = "Farmer Reviewable";

        //DOA report states
        public const string DOA_Approved = "DOA Approved";
        public const string DOA_Rejected = "DOA Rejected";
        //Keels report states
        public const string Keells_Approved = "Keells Approved";
        public const string Keells_Rejected = "Keells Rejected";
    }
}
