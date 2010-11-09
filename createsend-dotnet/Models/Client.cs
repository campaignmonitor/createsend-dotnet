using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace createsend_dotnet
{
    public class Client
    {
        public string ClientID { get; set; }
        public string Name { get; set; }
    }

    [XmlRoot("Clients")]
    public class Clients : EnumerableCollection<Client> { }
}
