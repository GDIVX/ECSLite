using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ECSLite
{
    /// <summary>
    /// A component that can tag 
    /// </summary>
    [CreateAssetMenu(fileName = "EntityTag", menuName = "ECS/EntityTag", order = 0)]
    public class EntityTag : DataComponent
    {
        [SerializeField] string _tag;

        public string Tag { get => _tag; private set => _tag = value; }

        public bool CompareTag(string tag)
        {
            return _tag.Equals(tag, System.StringComparison.Ordinal);
        }
    }
}