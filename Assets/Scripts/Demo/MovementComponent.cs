using Assets.Scripts.ECSLite;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Demo
{
    [CreateAssetMenu(fileName = "MovementComponent", menuName = "ECS/Components/Movement")]
    public class MovementComponent : DataComponent
    {
        public Vector3 Velocity;
        public float Speed;
        public float maxSpeed;
        public float acceleration;

    }
}