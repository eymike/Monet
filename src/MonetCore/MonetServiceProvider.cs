using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;

namespace MonetCore
{
    public class ServiceAtribute : Attribute
    {
        
    }

    public class ServiceDependencyAttribute : Attribute
    {
        public Type DependsOn { get; set; }
    }

    public class MonetServiceProvider : IServiceProvider
    {
        Dictionary<Type, object> m_services = new Dictionary<Type, object>();

        public MonetServiceProvider()
        {
            var servicesToCreate = new List<Type>(
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                where type.IsDefined(typeof(ServiceAtribute), true)
                select type);

            foreach (var serviceType in servicesToCreate)
            {
                var service = serviceType.GetConstructor(new Type[]{typeof(MonetServiceProvider)}).Invoke(new object[]{this});
            }
        }

        public object GetService(Type serviceType)
        {
            return m_services[serviceType];
        }

    }
}
