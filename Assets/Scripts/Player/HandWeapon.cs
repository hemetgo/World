using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWeapon : HandItem
{
	[SerializeField] Transform _firePoint;

	bool _isRecharging;
	float _rechargingTimer;

	public float RechargingPercent => _rechargingTimer / WeaponSettings.RechargeTime;

	public int CurrentBullets { get; private set; }

	public WeaponSettings WeaponSettings => ItemSettings as WeaponSettings;
	public bool HaveBullets => CurrentBullets > 0;

	private void Awake()
	{
		InstaRecharge();
	}

	private void Update()
	{
		if (_isRecharging)
		{
			_rechargingTimer += Time.deltaTime;
			if ( _rechargingTimer >= WeaponSettings.RechargeTime)
			{
				_isRecharging = false;
				CurrentBullets = WeaponSettings.MagazineCapacity;
				GameEvents.Player.OnRechargingComplete?.Invoke(this);
			}
		}
	}

	public override void OnActivated()
	{
		GameEvents.Player.OnChangeWeapon?.Invoke(this);
	}

	public void Fire(EnemyController enemy)
	{
		if (CurrentBullets <= 0) return;

		Projectile projectile = Instantiate(WeaponSettings.ProjectilePrefab);
		GameObject muzzleFlash = Instantiate(WeaponSettings.MuzzleFlashPrefab, _firePoint.position, transform.rotation);
		projectile.Setup(_firePoint.position, enemy.TargetPoint.position, WeaponSettings.Damage, WeaponSettings.ProjectileSpeed);

		CurrentBullets--;

		GameEvents.Player.OnFire?.Invoke(this);

		if (CurrentBullets <= 0)
		{
			StartRecharge();
		}
	}

	public void InstaRecharge()
	{
		CurrentBullets = WeaponSettings.MagazineCapacity;
		GameEvents.Player.OnRechargingComplete?.Invoke(this);
	}

	public void StartRecharge()
	{
		_isRecharging = true;

		_rechargingTimer = 0;

		GameEvents.Player.OnRechargingStart?.Invoke(this);
	}
}
