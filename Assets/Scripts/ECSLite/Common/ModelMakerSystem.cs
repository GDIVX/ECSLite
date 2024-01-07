using Assets.Scripts.Demo;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ECSLite.Common
{
    public class ModelMakerSystem : GameSystem
    {
        protected override bool ShouldProcessEntity(Entity entity)
        {
            return entity.HasComponent<ModelComponent>()
                && entity.HasTag("HasGameObject")
                && !entity.GetComponent<ModelComponent>().IsInitialized;
        }

        private void Start()
        {
            UpdateEntities();
        }

        public override void OnComponentAdded(Entity entity, IComponent component)
        {
            base.OnComponentAdded(entity, component);

            OnUpdate(entity);
        }

        private void Update()
        {
            //look for entities we missed
            if (entitiesToProcess.Count != 0)
            {
                UpdateEntities();
            }
        }

        protected override void OnUpdate(Entity entity)
        {
            if (entity.GetComponent<ModelComponent>().IsInitialized)
            {
                //we already handled this entity. Remove it from the system
                RemoveEntity(entity);
                return;
            }

            //instantiate a model as a child of the entity's root
            var component = entity.GetComponent<ModelComponent>();
            var prefab = component.Model;
            var model = Instantiate(prefab, entity.GetRootGameObject().transform);
            model.name = $"{entity.ToString()}_Model";

            //toggle initialized
            component.IsInitialized = true;
        }
    }
}