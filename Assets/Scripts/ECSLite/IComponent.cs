using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ECSLite
{
    public interface IComponent 
    {
        public IComponent Clone();
    }
}