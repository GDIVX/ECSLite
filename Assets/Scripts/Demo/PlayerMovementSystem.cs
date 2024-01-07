using Assets.Scripts.ECSLite;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Demo
{
    public class PlayerMovementSystem : GameSystem
    {
        protected override bool ShouldProcessEntity(Entity entity)
        {
            return entity.HasComponent<MovementComponent>()
                && entity.HasComponent<PositionComponent>()
                && entity.HasTag("Player");
        }

        protected override void UpdateEntity(Entity entity)
        {
            var movementData = entity.GetComponent<MovementComponent>();
            var positionData = entity.GetComponent<PositionComponent>();

            //get input from player
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            //move the player
            positionData.Position.x += horizontal * movementData.Speed * Time.deltaTime;
            positionData.Position.z += vertical * movementData.Speed * Time.deltaTime;

        }
    }
}