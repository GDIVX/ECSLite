using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.ECSLite;

[CreateAssetMenu(fileName = "HealthComponent", menuName = "ECS/Components/HealthComponent")]
public class HealthComponent : DataComponent
{
    public int CurrentHealth;
    public int MaxHealth;

}
