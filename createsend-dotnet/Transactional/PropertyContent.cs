using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace createsend_dotnet.Transactional
{
    public class PropertyContent
    {
        public string Html { get; set; }
        public string Text { get; set; }
        public string[] EmailVariables { get; set; }
        public bool InlineCss { get; set; }
    }
}
