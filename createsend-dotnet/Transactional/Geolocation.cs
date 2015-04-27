using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace createsend_dotnet.Transactional
{
    public class Geolocation
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }
}
