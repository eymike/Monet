using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonetCore.World
{
    public class Entity
    {
        public Guid ID { get; private set; }
        private Dictionary<Type, IEntityComponent> _components = new Dictionary<Type,IEntityComponent>();

        Entity()
        {
            ID = Guid.NewGuid();
        }

        public ComponentType GetComponent<ComponentType>()
        {
            return (ComponentType)_components[typeof(ComponentType)];
        }

        public IEnumerable<IEntityComponent> GetComponents()
        {
            return _components.Values;
        }

        public void AddComponent(IEntityComponent component)
        {
            _components.Add(component.ComponentType, component);
        }

        public void RemoveComponent(IEntityComponent component)
        {
            RemoveComponent(component.ComponentType);
        }

        public void RemoveComponent(Type componentType)
        {
            _components.Remove(componentType);
        }

        public bool HasComponent<ComponentType>()
        {
            return _components.ContainsKey(typeof(ComponentType));
        }
    }

    public class EntityManager
    {
        private List<Entity> _entities = new List<Entity>();

        public IEnumerable<Entity> GetEntities()
        {
            return _entities;
        }

        public IEnumerable<Entity> GetEntitiesWithComponent<ComponentType>()
        {
            return from entity in _entities
                   where entity.HasComponent<ComponentType>()
                   select entity;
        }

        public IEnumerable<ComponentType> GetAllComponents<ComponentType>()
        {
            return from entity in _entities
                   where entity.HasComponent<ComponentType>()
                   select entity.GetComponent<ComponentType>();
        }

        public Entity GetEntity(Guid id)
        {
            return (from entity in _entities
                    where entity.ID == id
                    select entity).First<Entity>();
        }
    }
}
