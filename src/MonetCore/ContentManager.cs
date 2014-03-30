using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonetCore
{
    public class ContentManager
    {
        private Dictionary<Guid, Uri> m_resources = new Dictionary<Guid, Uri>();

        [ServiceAtribute(Name="ContentManager")]
        [ServiceDependencyAttribute(DependsOn = typeof(GraphicsDeviceService))]
        public ContentManager()
        {

        }
    }
}
