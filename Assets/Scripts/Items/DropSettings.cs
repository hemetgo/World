using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drop", menuName = "Survival/Item/Drop")]
public class DropSettings : ScriptableObject
{
    [Range(0, 1)] public float DropChance;
    public List<RewardDrop> Drops = new List<RewardDrop>();
}
