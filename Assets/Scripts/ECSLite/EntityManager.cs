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
            var id = _entities.Count;
            var entity = new Entity(id);

            _entities.Add(id, entity);

            OnEntityCreated?.Invoke(entity);

            return entity;
        }

        internal void DestroyEntity(Entity entity)
        {
            _entities.Remove(entity.ID);
            OnEntityDeleted?.Invoke(entity);
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
    }
}