using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;

namespace MonetCore
{
    public class ServiceAtribute : Attribute
    {
        public string Name { get; set; }
    }

    public class ServiceDependencyAttribute : Attribute
    {
        public Type DependsOn { get; set; }
    }

    public class MonetServiceProvider : IServiceProvider
    {
        Dictionary<Type, object> m_services = new Dictionary<Type, object>();

        public MonetServiceProvider(GraphicsDeviceService bootStrap)
        {
            m_services.Add(typeof(GraphicsDeviceService), bootStrap);

            var ca = typeof(ContentManager).GetCustomAttributes(true);

            var servicesToCreate = new LinkedList<Type>(
                from assembly in AppDomain.CurrentDomain.GetAssemblies().AsParallel()
                from type in assembly.GetTypes()
                where Attribute.IsDefined(type, typeof(ServiceAtribute), true)
                select type);

            while(servicesToCreate.Count > 0)
            {
                var front = servicesToCreate.First.Value;

                //Can we satisfy all dependencies?
                bool depsSatisfied = true;
                foreach( var attr in front.GetCustomAttributes(true))
                {
                    var depAttr = attr as ServiceDependencyAttribute;
                    if(depAttr != null && !m_services.ContainsKey(depAttr.DependsOn))
                    {
                        depsSatisfied = false;
                        break;
                    }
                }

                if(depsSatisfied)
                {
                    Monet.LogMsg(string.Format("Inserting {0} into services collection", front.Name));
                    m_services.Add(front, front.GetConstructor(new Type[] { typeof(MonetServiceProvider) }).Invoke(new object[] { this }));
                    servicesToCreate.RemoveFirst();
                }
                else
                {
                    var node = servicesToCreate.First;
                    servicesToCreate.RemoveFirst();
                    servicesToCreate.AddLast(node);
                }
            }
        }

        public object GetService(Type serviceType)
        {
            return m_services[serviceType];
        }

    }
}
