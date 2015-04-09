using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MaltegoTransformNet.Core
{
	public class IconURL
	{
		[XmlText]
		public string Value {
			get;
			set;
		}
	}
}
