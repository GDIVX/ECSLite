using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ECSLite
{
    public abstract class GameSystem : MonoBehaviour
    {
        [SerializeField] private List<Entity> entitiesToProcess = new List<Entity>();

        private void Awake()
        {
            //register this system with the entity manager
            EntityManager.Instance.RegisterSystem(this);
        }

        public void OnComponentAdded(Entity entity, IComponent component)
        {
            // Check if the entity meets the criteria for processing by this system
            // If it does, add it to the entitiesToProcess list
            AddEntity(entity);
        }

        public void OnComponentRemoved(Entity entity, IComponent component)
        {
            // If the entity no longer meets the criteria, remove it from the list
            if (!ShouldProcessEntity(entity))
            {
                RemoveEntity(entity);
            }
        }

        public void OnEntityCreated(Entity entity)
        {
            AddEntity(entity);
        }

        public void OnEntityDeleted(Entity entity)
        {
            // Remove the entity from the list
            RemoveEntity(entity);
        }

        protected void AddEntity(Entity entity)
        {
            // Check if the entity should be processed by this system
            if (!ShouldProcessEntity(entity)) return;

            //If we already have this entity, return
            if (entitiesToProcess.Contains(entity))
            {
                return;
            }

            // Subscribe to the entity's events
            entity.OnComponentRemoved += OnComponentRemoved;

            entitiesToProcess.Add(entity);
        }

        protected void RemoveEntity(Entity entity)
        {
            //do we have this entity?
            if (entitiesToProcess.Contains(entity))
            {
                //unsubscribe to the entities events
                entity.OnComponentRemoved -= OnComponentRemoved;

                entitiesToProcess.Remove(entity);
            }
        }

        private void Update()
        {
            for (int i = 0; i < entitiesToProcess.Count; i++)
            {
                Entity entity = entitiesToProcess[i];
                UpdateEntity(entity);
            }
        }

        protected abstract void UpdateEntity(Entity entity);

        // Implement this method to define the criteria for processing an entity
        protected abstract bool ShouldProcessEntity(Entity entity);
    }
}
