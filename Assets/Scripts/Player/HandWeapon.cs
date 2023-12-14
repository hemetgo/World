using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWeapon : HandItem
{
	[SerializeField] Transform _firePoint;

	public WeaponSettings WeaponSettings => ItemSettings as WeaponSettings;

	public void Fire(Enemy enemy)
	{
		Projectile projectile = Instantiate(WeaponSettings.ProjectilePrefab);
		projectile.Setup(_firePoint.position, enemy.transform.position + Vector3.up, WeaponSettings.Damage, WeaponSettings.ProjectileSpeed);
	}
}
