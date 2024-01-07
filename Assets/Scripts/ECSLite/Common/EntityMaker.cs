using Assets.Scripts.ECSLite;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Demo
{
    public class EntityMaker : MonoBehaviour
    {
        [SerializeField] EntityDefinition _entityDef;
        private void Start()
        {
            _entityDef.CreateEntity();
        }
    }
}