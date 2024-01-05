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
			if (!HaveBullets || IsReloading) return false;


			return true;
		}
	}

	private void OnEnable()
	{
		GameEvents.Inputs.OnReload += StartReload;
		InventoryService.OnInventoryChanged += OnUpdateAmmo;
	}

	private void OnDisable()
	{
		GameEvents.Inputs.OnReload -= StartReload;
		InventoryService.OnInventoryChanged -= OnUpdateAmmo;
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
	}

	public override void OnUnequip()
	{
		base.OnUnequip();

		if (IsReloading)
		{
			IsReloading = false;
			GameEvents.Player.StopReloading?.Invoke(this);
		}
	}

	public override void OnUse()
	{
		base.OnUse();
		Fire(PlayerController.Instance.transform.forward);
	}

	public void Fire(Vector3 direction)
	{
		if (CurrentBullets <= 0) return;

		Projectile projectile = Instantiate(FireWeaponSettings.ProjectilePrefab);
		GameObject muzzleFlash = Instantiate(FireWeaponSettings.MuzzleFlashPrefab, _firePoint.position, transform.rotation);
		projectile.Setup(_firePoint.position, direction, FireWeaponSettings);

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
		GameEvents.Player.CompleteReloading?.Invoke(this);
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

				GameEvents.Player.CompleteReloading?.Invoke(this);
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

		GameEvents.Player.StartReloading?.Invoke(this);
	}
}
