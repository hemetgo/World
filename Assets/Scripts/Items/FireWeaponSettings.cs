using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Survival/Item/Weapon")]
public class FireWeaponSettings : WeaponSettings
{
	[field: SerializeField] public ItemSettings Ammo { get; set; }
	[field: SerializeField] public int MagazineCapacity { get; set; }
	[field: SerializeField] public float RechargeTime { get; set; }
	[field: SerializeField] public float ProjectileSpeed { get; set; }
	[field: SerializeField] public Projectile ProjectilePrefab { get; set; }
	[field: SerializeField] public GameObject MuzzleFlashPrefab { get; set; }

	public int GetReserveAmmo()
	{
		if (InventoryService.TryGetItem(Ammo.SaveID, out ItemData item) == false) 
			return 0;

		return item.Amount;
	}

	public int DiscountAmmoFromInventory(int currentBullets)
	{
		if (InventoryService.TryGetItem(Ammo.SaveID, out ItemData item) == false) 
			return 0;

		int ammoToDiscount = item.Amount;
		if (item.Amount >= MagazineCapacity) ammoToDiscount = MagazineCapacity - currentBullets;

		InventoryService.RemoveItem(Ammo, ammoToDiscount);
		return ammoToDiscount;
	}
}
