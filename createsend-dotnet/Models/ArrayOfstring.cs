using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace createsend_dotnet
{
    [XmlRoot("ArrayOfstring")]
    public class ArrayOfstring : EnumerableCollection<string> { }
}
