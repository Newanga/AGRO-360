using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agro360.Models
{
    public class ZipCodeAddress

    {
        public bool success { get; set; }
        public Location[] location { get; set; }
    }

    public class Location
    {
        public string country { get; set; }
        public string countryCode2 { get; set; }
        public string countryCode3 { get; set; }
        public string state { get; set; }
        public string stateCode2 { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string zipCode { get; set; }
        public string city { get; set; }
    }

}
