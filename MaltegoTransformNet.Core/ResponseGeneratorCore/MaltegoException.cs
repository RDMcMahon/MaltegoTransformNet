using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MaltegoTransformNet.Core
{
	[XmlRoot(ElementName="Exception")]
	public class MaltegoException
	{
		[XmlText]
		public string Value {
			get;
			set;
		}
	}
}
