using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MaltegoTransformNet.Core
{
    public class MaltegoTransformResponseMessage
    {
        public List<Entity> Entities { get; set; }
		public List<UIMessage> UIMessages { get; set; }
    }
}
