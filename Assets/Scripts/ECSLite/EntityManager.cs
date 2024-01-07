using System;
using System.Collections.Generic;

namespace Assets.Scripts.ECSLite
{
    /// <summary>
    /// The EntityManager is responsible for creating and destroying entities.
    /// </summary>
    internal class EntityManager
    {
        public event Action<Entity> OnEntityCreated;
        public Action<Entity> OnEntityInitialized;
        public event Action<Entity> OnEntityDeleted;

        //singleton
        private static EntityManager _instance;
        public static EntityManager Instance => _instance ?? (_instance = new EntityManager());

        //Dictionary of entities
        Dictionary<int, Entity> _entities = new();

        
        internal Entity CreateEntity()
        {
            var entity = new Entity();

            _entities.Add(entity.ID, entity);

            OnEntityCreated?.Invoke(entity);

            return entity;
        }

        internal void DestroyEntity(Entity entity)
        {
            _entities.Remove(entity.ID);
            OnEntityDeleted?.Invoke(entity);
        }

        public Entity Find(int id)
        {
            return _entities[id];
        }

        public Entity[] FindAllEntitiesWithComponent<T>() where T : IComponent
        {
            var entities = new List<Entity>();

            foreach (var entity in _entities.Values)
            {
                if (entity.HasComponent<T>())
                {
                    entities.Add(entity);
                }
            }

            return entities.ToArray(); ;
        }

        public Entity[] FindAllEntitiesWithTag(string tag)
        {
            var entities = new List<Entity>();

            foreach (var entity in _entities.Values)
            {
                if (entity.HasTag(tag))
                {
                    entities.Add(entity);
                }
            }

            return entities.ToArray(); ;
        }

        internal void RegisterSystem(GameSystem gameSystem)
        {
            //subscribe to the events
            OnEntityInitialized += gameSystem.OnEntityCreated;
            OnEntityDeleted += gameSystem.OnEntityDeleted;

            //iterate through all entities and add those that meet the criteria
            foreach (var entity in _entities.Values)
            {
                gameSystem.OnEntityCreated(entity);
                entity.OnComponentAdded += gameSystem.OnComponentAdded;
            }
        }

        public int GenerateUniqueId()
        {
            int id;
            do
            {
                id = Guid.NewGuid().GetHashCode(); // Use Guid to reduce the risk of collisions
            }
            while (_entities.ContainsKey(id));

            return id;
        }
    }
}