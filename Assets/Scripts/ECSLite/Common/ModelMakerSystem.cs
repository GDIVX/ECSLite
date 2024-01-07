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

        protected override void UpdateEntity(Entity entity)
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