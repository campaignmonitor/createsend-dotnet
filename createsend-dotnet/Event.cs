using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace createsend_dotnet
{
    public class Event : CreateSendBase
    {
        public string ClientID { get; set; }

        public Event(AuthenticationDetails auth, string clientID)
            : base(auth)
        {
            ClientID = clientID;
        }

        public static BasicEventResult Track(
            AuthenticationDetails auth,
            string clientID,
            string eventName,
            string emailAddress,
            object data)
        {
            return HttpHelper.Post<BasicEvent, BasicEventResult>(
                auth, string.Format("/events/{0}/track", clientID), null,
                new BasicEvent()
                {
                    EventName = eventName,
                    ContactID = new ContactID { Email = emailAddress },
                    Data = data
                });
        }
    }
}
