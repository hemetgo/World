using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Survival/Item/Weapon")]
public class WeaponSettings : ItemSettings
{
	[field: SerializeField] public int Damage { get; set; }
	[field: SerializeField] public float ProjectileSpeed { get; set; }
	[field: SerializeField] public float Range { get; set; }
	[field: SerializeField, Range(.8f, 10)] public float FireRate { get; set; }

	[field: SerializeField] public Projectile ProjectilePrefab { get; set; }
	[field: SerializeField] public GameObject MuzzleFlashPrefab { get; set; }
}
