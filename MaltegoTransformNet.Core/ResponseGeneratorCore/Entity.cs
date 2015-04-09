using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MaltegoTransformNet.Core
{
    public class Entity
    {
        [XmlAttribute]
        public string Type { get; set; }

        [XmlElement]
        public string Value { get; set; }

        [XmlElement]
        public int Weight { get; set; }

		public IconURL IconURL {
			get;
			set;
		}

		public List<Field> AdditionalFields {
			get;
			set;
		}
    }
}
