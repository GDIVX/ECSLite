using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ECSLite
{
    public abstract class DataComponent : ScriptableObject, IComponent
    {
        public IComponent Clone()
        {
            var clone = Instantiate(this);
            clone.Initialize();
            return clone;
        }

        virtual public void Initialize() { }
    }
}