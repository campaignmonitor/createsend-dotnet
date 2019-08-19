using System;

namespace createsend_dotnet.Models
{
    public class JourneyEmailRecipient
    {
        public string EmailAddress { get; set; }
        public DateTime SentDate { get; set; }
    }

    public class JourneyEmailOpenDetail : JourneyEmailDetailWithGeoBase
    {
        public string EmailAddress { get; set; }
        public DateTime Date { get; set; }
    }

    public class JourneyEmailUnsubscribeDetail
    {
        public string EmailAddress { get; set; }
        public DateTime Date { get; set; }
        public string IPAddress { get; set; }
    }

    public class JourneyEmailClickDetail : JourneyEmailDetailWithGeoBase
    {
        public string EmailAddress { get; set; }
        public DateTime Date { get; set; }
        public string URL { get; set; }
    }

    public class JourneyEmailBounceDetail
    {
        public string EmailAddress { get; set; }
        public string BounceType { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
    }

    public class JourneyEmailDetailWithGeoBase
    {
        public string IPAddress { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }
}
