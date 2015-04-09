using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MaltegoTransformNet.Core
{
	public class Field
	{
		[XmlAttribute]
		public string Name {
			get;
			set;
		}

		[XmlAttribute]
		public string DisplayName {
			get;
			set;
		}

		[XmlAttribute]
		public string MatchingRule {
			get;
			set;
		}

		[XmlText]
		public string Value {
			get;
			set;
		}
	}
}
