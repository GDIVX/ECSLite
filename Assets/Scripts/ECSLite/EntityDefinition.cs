using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ECSLite
{
    /// <summary>
    /// EntityDefinition is a ScriptableObject that can be used to create entities with predefined components.
    /// Note: This is not for runtime use. It is only for creating entities in the editor.
    /// </summary>
    [CreateAssetMenu(fileName = "EntityDefinition", menuName = "ECS/EntityDefinition")]
    public class EntityDefinition : ScriptableObject
    {
        [SerializeField] List<DataComponent> components;

        public Entity CreateEntity()
        {
            var entity = EntityManager.Instance.CreateEntity();

            foreach (var component in components)
            {
                entity.AddComponent(component.Clone());
            }

            EntityManager.Instance.OnEntityInitialized?.Invoke(entity);

            return entity;
        }
    }
}