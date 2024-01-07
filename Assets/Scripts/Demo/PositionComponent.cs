using Assets.Scripts.ECSLite;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Demo
{
    [CreateAssetMenu(fileName = "PositionComponent", menuName = "ECS/Components/Position")]
    public class PositionComponent : DataComponent
    {
        public Vector3 Position;
    }
}