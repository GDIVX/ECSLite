using Assets.Scripts.ECSLite;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Demo
{
    [CreateAssetMenu(fileName = "ModelComponent", menuName = "ECS/Components/Model")]
    public class ModelComponent : DataComponent
    {
        public GameObject Model;
        public bool IsInitialized { get; set; } = false;

    }
}