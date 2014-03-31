using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonetCore
{
    [ServiceAtribute(Name = "ContentManager")]
    [ServiceDependencyAttribute(DependsOn = typeof(GraphicsDeviceService))]
    public class ContentManager
    {
        private readonly Dictionary<Guid, Uri> m_resources = new Dictionary<Guid, Uri>();
        private readonly Dictionary<Guid, object> m_loadedResources = new Dictionary<Guid, object>();
        private readonly MonetServiceProvider m_services;
        
        public ContentManager(MonetServiceProvider serviceProvider)
        {
            m_services = serviceProvider;

            
        }

        public AssetType Load<AssetType>(Guid id)
        {
            if(m_loadedResources.ContainsKey(id))
            {
                return (AssetType)m_loadedResources[id];
            }
            else
            {
                throw new ArgumentException("Resource not loaded.");
            }
        }
    }
}
