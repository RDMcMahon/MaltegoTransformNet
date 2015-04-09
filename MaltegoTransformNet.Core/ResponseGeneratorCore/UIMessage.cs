using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MaltegoTransformNet.Core
{
	public class UIMessage
	{
		//Inform, FatalError, PartialError, Debug
		[XmlAttribute]
		public string MessageType {
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
