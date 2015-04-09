using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MaltegoTransformNet.Core
{
	public class MaltegoTransformExceptionMessage
	{
		public List<MaltegoException> Exceptions {
			get;
			set;
		}
	}
}
