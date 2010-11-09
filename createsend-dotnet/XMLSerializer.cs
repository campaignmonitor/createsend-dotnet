﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace createsend_dotnet
{
    public class XMLSerializer
    {
        public static T Deserialize<T>(string serialized)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringReader stream = new StringReader(serialized))
            using (XmlReader reader = XmlReader.Create(stream))
            {
                
                return (T)serializer.Deserialize(reader);
            }
        }

        public static string Serialize<T>(T model)
        {
            StringWriter writer = new StringWriter(new StringBuilder());
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            serializer.Serialize(writer, model);

            return writer.ToString();
        }
    }
}