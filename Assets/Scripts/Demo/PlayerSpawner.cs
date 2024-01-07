using Assets.Scripts.ECSLite;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Demo
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] EntityDefinition _playerDef;
        private void Start()
        {
            _playerDef.CreateEntity();
        }
    }
}