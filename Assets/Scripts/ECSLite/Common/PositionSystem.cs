using Assets.Scripts.ECSLite;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Demo
{
    public class PositionSystem : GameSystem
    {
        protected override bool ShouldProcessEntity(Entity entity)
        {
            //we should process any entity with a position component and a model component
            return entity.HasComponent<PositionComponent>()
                && entity.HasTag("HasGameObject");
        }

        protected override void UpdateEntity(Entity entity)
        {
            //get the position component
            var positionComponent = entity.GetComponent<PositionComponent>();

            //update the entity root game object
            entity.GetRootGameObject().transform.position = positionComponent.Position;
        }
    }
}