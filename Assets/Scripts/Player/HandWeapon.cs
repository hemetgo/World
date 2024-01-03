using UnityEngine;

public class HandWeapon : HandItem
{
	[SerializeField] Transform _firePoint;

	public bool IsRecharging { get; private set; }

	float _rechargingTimer;

	public float RechargingPercent => _rechargingTimer / WeaponSettings.RechargeTime;

	public int CurrentBullets { get; private set; }

	public WeaponSettings WeaponSettings => ItemSettings as WeaponSettings;
	public bool HaveBullets => CurrentBullets > 0;

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

	public override void OnActivated()
	{
		GameEvents.Player.OnChangeWeapon?.Invoke(this);
		InventoryService.OnInventoryChanged += OnUpdateAmmo;
	}

	public override void OnDeactivated()
	{
		if (IsRecharging)
		{
			IsRecharging = false;
			GameEvents.Player.OnRechargingStop?.Invoke(this);
		}

		InventoryService.OnInventoryChanged -= OnUpdateAmmo;
	}

	public void Fire(EnemyController enemy)
	{
		if (CurrentBullets <= 0) return;

		Projectile projectile = Instantiate(WeaponSettings.ProjectilePrefab);
		GameObject muzzleFlash = Instantiate(WeaponSettings.MuzzleFlashPrefab, _firePoint.position, transform.rotation);
		projectile.Setup(_firePoint.position, PlayerController.Instance.MouseInput, WeaponSettings.Damage, WeaponSettings.ProjectileSpeed);

		CurrentBullets--;

		GameEvents.Player.OnAmmoUpdate?.Invoke(this);
		GameEvents.Player.OnFire?.Invoke(this);
	}

	public void InstaRecharge()
	{
		CurrentBullets = WeaponSettings.MagazineCapacity;
		GameEvents.Player.OnRechargingComplete?.Invoke(this);
	}

	void RechargingControl()
	{
		if (IsRecharging)
		{
			_rechargingTimer += Time.fixedDeltaTime;

			if (_rechargingTimer >= WeaponSettings.RechargeTime)
			{
				IsRecharging = false;

				int rechargedAmmo = WeaponSettings.DiscountAmmoFromInventory();
				CurrentBullets = rechargedAmmo;

				GameEvents.Player.OnRechargingComplete?.Invoke(this);
			}
		}
		else if (CurrentBullets <= 0 && WeaponSettings.GetReserveAmmo() > 0)
		{
			StartRecharge();
		}
	}

	public void StartRecharge()
	{
		IsRecharging = true;

		_rechargingTimer = 0;

		GameEvents.Player.OnRechargingStart?.Invoke(this);
	}
}
