using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWeapon : HandItem
{
	[SerializeField] Transform _firePoint;

	public WeaponSettings WeaponSettings => ItemSettings as WeaponSettings;

	public void Fire(EnemyController enemy)
	{
		Projectile projectile = Instantiate(WeaponSettings.ProjectilePrefab);
		GameObject muzzleFlash = Instantiate(WeaponSettings.MuzzleFlashPrefab, _firePoint.position, transform.rotation);
		projectile.Setup(_firePoint.position, enemy.TargetPoint.position, WeaponSettings.Damage, WeaponSettings.ProjectileSpeed);
	}
}
