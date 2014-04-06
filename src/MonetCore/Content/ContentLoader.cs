using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonetCore.Content
{
    public interface IContentLoader
    {
        object Load(Uri uri);
        Type AssetType { get; }
    }

    public abstract class ContentLoader
    {
        protected readonly MonetServiceProvider m_services;

        public ContentLoader(MonetServiceProvider services)
        {
            m_services = services;
        }
    }
}
