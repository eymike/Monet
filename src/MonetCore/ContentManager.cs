using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using System.Windows.Forms;

using MonetCore.Content;

namespace MonetCore
{
    [ServiceAtribute(Name = "ContentManager")]
    [ServiceDependencyAttribute(DependsOn = typeof(GraphicsDeviceService))]
    public class ContentManager
    {
        private readonly Dictionary<Uri, object> m_loadedResources = new Dictionary<Uri, object>();
        private readonly Dictionary<Type, IContentLoader> m_contentLoaders = new Dictionary<Type, IContentLoader>();
        private readonly string m_rootPath;
        private readonly MonetServiceProvider m_services;
        
        public ContentManager(MonetServiceProvider serviceProvider)
        {
            m_services = serviceProvider;
            m_rootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            foreach(
                var contentTypeLoaderType in
                from assembly in AppDomain.CurrentDomain.GetAssemblies().AsParallel()
                from type in assembly.GetTypes()
                where typeof(IContentLoader).IsAssignableFrom(type)
                where type.IsClass || type.IsValueType
                select type)
            {
                Monet.LogMsg(string.Format("ContentManager.ContentManager: Inserting {0} into type loader collection", contentTypeLoaderType.Name));
                var constructor = contentTypeLoaderType.GetConstructor(
                    new Type[] { typeof(MonetServiceProvider) });
                var loader = constructor.Invoke(new object[] { m_services }) as IContentLoader;

                m_contentLoaders.Add(loader.AssetType, loader);
            }
        }

        public AssetType Load<AssetType>(string relativePath)
        {
            var uri = new Uri(Path.Combine(m_rootPath, relativePath));

            if(m_loadedResources.ContainsKey(uri))
            {
                return (AssetType)m_loadedResources[uri];
            }
            else
            {
                var loader = m_contentLoaders[typeof(AssetType)];
                var loadedResource = loader.Load(uri);
                m_loadedResources.Add(uri, loadedResource);
                return (AssetType)loadedResource;
            }
        }
    }
}
