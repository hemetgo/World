using UnityEngine;

public class HandWeapon : HandItem
{
	[SerializeField] Transform _firePoint;

	public bool IsReloading { get; private set; }

	float _rechargingTimer;

	public float RechargingPercent => _rechargingTimer / WeaponSettings.RechargeTime;

	public int CurrentBullets { get; private set; }

	public WeaponSettings WeaponSettings => ItemSettings as WeaponSettings;
	public bool HaveBullets => CurrentBullets > 0;

	private void OnEnable()
	{
		GameEvents.Inputs.OnReload += StartReload;
	}

	private void OnDisable()
	{
		GameEvents.Inputs.OnReload -= StartReload;
	}

	private void Awake()
	{
		InstaRecharge();
	}

	private void FixedUpdate()
	{
		RechargingControl();
	}

	void OnUpdateAmmo()
	{
		GameEvents.Player.OnAmmoUpdate?.Invoke(this);
	}

	public override void OnEquip()
	{
		GameEvents.Player.OnChangeWeapon?.Invoke(this);
		InventoryService.OnInventoryChanged += OnUpdateAmmo;
	}

	public override void OnUnequip()
	{
		if (IsReloading)
		{
			IsReloading = false;
			GameEvents.Player.OnRechargingStop?.Invoke(this);
		}

		InventoryService.OnInventoryChanged -= OnUpdateAmmo;
	}

	public override void OnUse()
	{
		base.OnUse();
		Fire(InputHelper.GetRelativeMouseWorldPosition(transform));
	}

	public void Fire(Vector3 targetPosition)
	{
		if (CurrentBullets <= 0) return;

		Projectile projectile = Instantiate(WeaponSettings.ProjectilePrefab);
		GameObject muzzleFlash = Instantiate(WeaponSettings.MuzzleFlashPrefab, _firePoint.position, transform.rotation);
		projectile.Setup(_firePoint.position, targetPosition, WeaponSettings.Damage, WeaponSettings.ProjectileSpeed);

		CurrentBullets--;

		GameEvents.Player.OnAmmoUpdate?.Invoke(this);
		GameEvents.Player.OnFire?.Invoke(this);
	}

	public void Fire(EnemyController enemy)
	{
		Fire(enemy.TargetPoint.position);
	}

	public void InstaRecharge()
	{
		CurrentBullets = WeaponSettings.MagazineCapacity;
		GameEvents.Player.OnRechargingComplete?.Invoke(this);
	}

	void RechargingControl()
	{
		if (IsReloading)
		{
			_rechargingTimer += Time.fixedDeltaTime;

			if (_rechargingTimer >= WeaponSettings.RechargeTime)
			{
				IsReloading = false;

				int rechargedAmmo = WeaponSettings.DiscountAmmoFromInventory(CurrentBullets);
				CurrentBullets += rechargedAmmo;

				GameEvents.Player.OnRechargingComplete?.Invoke(this);
			}
		}
		else if (CurrentBullets <= 0 && WeaponSettings.GetReserveAmmo() > 0)
		{
			StartReload();
		}
	}

	public void StartReload(bool reload = true)
	{
		if (!reload || CurrentBullets >= WeaponSettings.MagazineCapacity || WeaponSettings.GetReserveAmmo() <= 0) return;

		IsReloading = true;

		_rechargingTimer = 0;

		GameEvents.Player.OnRechargingStart?.Invoke(this);
	}
}
