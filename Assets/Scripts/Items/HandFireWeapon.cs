using UnityEngine;

public class HandFireWeapon : HandWeapon
{
	[SerializeField] Transform _firePoint;

	public bool IsReloading { get; private set; }

	float _rechargingTimer;

	public float RechargingPercent => _rechargingTimer / FireWeaponSettings.RechargeTime;

	public int CurrentBullets { get; private set; }

	public FireWeaponSettings FireWeaponSettings => ItemSettings as FireWeaponSettings;
	public bool HaveBullets => CurrentBullets > 0;

	public override bool IsUseEnabled
	{
		get
		{
			if (!HaveBullets) return false;


			return true;
		}
	}

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
		base.OnEquip();

		InventoryService.OnInventoryChanged += OnUpdateAmmo;
	}

	public override void OnUnequip()
	{
		base.OnUnequip();

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

		Projectile projectile = Instantiate(FireWeaponSettings.ProjectilePrefab);
		GameObject muzzleFlash = Instantiate(FireWeaponSettings.MuzzleFlashPrefab, _firePoint.position, transform.rotation);
		projectile.Setup(_firePoint.position, targetPosition, FireWeaponSettings);

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
		CurrentBullets = FireWeaponSettings.MagazineCapacity;
		GameEvents.Player.OnRechargingComplete?.Invoke(this);
	}

	void RechargingControl()
	{
		if (IsReloading)
		{
			_rechargingTimer += Time.fixedDeltaTime;

			if (_rechargingTimer >= FireWeaponSettings.RechargeTime)
			{
				IsReloading = false;

				int rechargedAmmo = FireWeaponSettings.DiscountAmmoFromInventory(CurrentBullets);
				CurrentBullets += rechargedAmmo;

				GameEvents.Player.OnRechargingComplete?.Invoke(this);
			}
		}
		else if (CurrentBullets <= 0 && FireWeaponSettings.GetReserveAmmo() > 0)
		{
			StartReload();
		}
	}

	public void StartReload(InputState inputState = InputState.Down)
	{
		if (inputState != InputState.Down || CurrentBullets >= FireWeaponSettings.MagazineCapacity || FireWeaponSettings.GetReserveAmmo() <= 0) return;

		IsReloading = true;

		_rechargingTimer = 0;

		GameEvents.Player.OnRechargingStart?.Invoke(this);
	}
}
