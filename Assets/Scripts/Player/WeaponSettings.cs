using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Survival/Item/Weapon")]
public class WeaponSettings : ItemSettings
{
    [SerializeField] public int Damage;
    [SerializeField] public float ProjectileSpeed;
    [SerializeField] public float Range;
    [SerializeField, Range(.8f, 10)] public float FireRate;

    [SerializeField] public Projectile ProjectilePrefab;
}
