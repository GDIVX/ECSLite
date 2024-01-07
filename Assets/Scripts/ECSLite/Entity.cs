using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ECSLite
{
    public class Entity
    {

        public int ID { get; private set; }
        public List<IComponent> Components { get; private set; } = new();

        GameObject _rootGameObject;

        public event Action<Entity, IComponent> OnComponentAdded;
        public event Action<Entity, IComponent> OnComponentRemoved;

        internal Entity(int id)
        {
            this.ID = id;
        }

        public static Entity Create()
        {
            return EntityManager.Instance.CreateEntity();
        }
        public Entity Clone()
        {
            //Create a new entity
            var entity = Create();

            //clone each component and attach to the new entity
            foreach (var component in Components)
            {
                IComponent newComponent = component.Clone();
                entity.AddComponent(newComponent);
            }

            EntityManager.Instance.OnEntityInitialized?.Invoke(entity);

            return entity;
        }

        public void AddComponent(IComponent component)
        {
            Components.Add(component);
            OnComponentAdded?.Invoke(this, component);
        }

        public void RemoveComponent(IComponent component)
        {
            Components.Remove(component);
            OnComponentRemoved?.Invoke(this, component);
        }

        public T GetComponent<T>() where T : IComponent
        {
            foreach (var component in Components)
            {
                if (component is T)
                {
                    return (T)component;
                }
            }
            return default;
        }

        public bool HasComponent<T>() where T : IComponent
        {
            foreach (var component in Components)
            {
                if (component is T)
                {
                    return true;
                }
            }
            return false;
        }

        public T GetOrAddComponent<T>() where T : IComponent, new()
        {
            var component = GetComponent<T>();
            if (component == null)
            {
                component = new T();
                AddComponent(component);
            }
            return component;
        }

        public T TryGetComponent<T>() where T : IComponent
        {
            if (HasComponent<T>())
            {
                return GetComponent<T>();
            }
            return default;
        }

        public T[] GetComponents<T>() where T : IComponent
        {
            var components = new List<T>();
            foreach (var component in Components)
            {
                if (component is T)
                {
                    components.Add((T)component);
                }
            }
            return components.ToArray();
        }

        public GameObject GetRootGameObject()
        {
            //Check if we have "HasGameObject" tag
            if (!HasTag("HasGameObject"))
            {
                return null;
            }
            //Do we have a game object instantiated?
            if (_rootGameObject == null)
            {
                //Instantiate a new game object
                _rootGameObject = new GameObject(ToString());
            }
            return _rootGameObject;
        }

        public bool HasTag(string tag)
        {
            //first, we need to check if we have any tag component
            var tags = GetComponents<EntityTag>();

            if (tags == null || tags.Length == 0) return false;

            //compare each tag until we find one that is equal to the input string
            foreach (var tagComponent in tags)
            {
                if (tagComponent.CompareTag(tag))
                {
                    return true;
                }
            }
            return false;
        }

        public void Destroy()
        {
            EntityManager.Instance.DestroyEntity(this);
            if (_rootGameObject != null)
            {
                GameObject.Destroy(_rootGameObject);
            }
        }


        public override string ToString()
        {
            return $"Entity {ID}";
        }

    }

}