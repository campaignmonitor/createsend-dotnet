﻿using System;

namespace createsend_dotnet.Transactional
{
    public class SubscriberAction
    {
        public EmailAddress EmailAddress { get; set; }
        public DateTimeOffset Date { get; set; }
        public string IpAddress { get; set; }
        public Geolocation Geolocation { get; set; }
        public MailClient MailClient { get; set; }
    }
}
