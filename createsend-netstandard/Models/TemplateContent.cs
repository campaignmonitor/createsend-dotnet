using System;
using System.Collections.Generic;
using System.Text;

namespace createsend_dotnet
{
    public class EditableField
    {
        public string Content;
        public string Alt;
        public string Href;
    }

    public class Repeater
    {
        public List<RepeaterItem> Items;
    }

    public class RepeaterItem
    {
        public string Layout;
        public List<EditableField> Singlelines;
        public List<EditableField> Multilines;
        public List<EditableField> Images;
    }

    public class TemplateContent
    {
        public List<EditableField> Singlelines;
        public List<EditableField> Multilines;
        public List<EditableField> Images;
        public List<Repeater> Repeaters;
    }
}
