using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace MaltegoTransformNet.Core
{
    public class MaltegoMessage
    {
        public MaltegoTransformResponseMessage MaltegoTransformResponseMessage { get; set; }
		public MaltegoTransformExceptionMessage MaltegoTransformExceptionMessage { get; set; }
    }
}
