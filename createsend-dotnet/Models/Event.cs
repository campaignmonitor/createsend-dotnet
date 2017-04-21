using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace createsend_dotnet
{
    public class ContactID
    {
        public string Email { get; set; }
    }

    public class BasicEvent
    {
        public ContactID ContactID { get; set; }
        public string EventName { get; set; }
        public object Data { get; set; }
    }

    public class BasicEventResult
    {
        public string EventID { get; set; }
    }
}
