using System;
using System.Collections.Generic;

public class WeaponInventoryService
{
    private static WeaponInventoryData _weaponInventoryData { get; set; }

	public static Action OnInventoryChanged;

	public static Dictionary<int, ItemData> Items => _weaponInventoryData.Items;

	private static void EnsureData()
    {
		if (_weaponInventoryData == null)
		{
			_weaponInventoryData = DataManager.Load<WeaponInventoryData>();
		}
	}

	private static void Save()
	{
		DataManager.Save(_weaponInventoryData);
	}

	public static ItemData GetItem(WeaponItemType type)
	{
		return _weaponInventoryData.GetItem(type);
	}

	public static void SetMeleeWeapon(ItemSettings meleeWeaponSettings)
	{
		EnsureData();
		_weaponInventoryData.SetMeleeWeapon(meleeWeaponSettings);
		Save();
		OnInventoryChanged?.Invoke();
	}

	public static void SetPistol(FireWeaponSettings pistolSettings)
    {
		EnsureData();
		_weaponInventoryData.SetPistol(pistolSettings, pistolSettings.MagazineCapacity);
		Save();
		OnInventoryChanged?.Invoke();
	}

	public static void SetHeavyWeapon(FireWeaponSettings heavyWeaponSettings)
	{
		EnsureData();
		_weaponInventoryData.SetHeavyWeapon(heavyWeaponSettings, heavyWeaponSettings.MagazineCapacity);
		Save();
		OnInventoryChanged?.Invoke();
	}

	public static void AddItem(ItemSettings itemSettings, int amount)
	{
		if (itemSettings == null) 
			return;

		EnsureData();
		_weaponInventoryData.AddItem(itemSettings, amount);
		Save();
		OnInventoryChanged?.Invoke();
	}

	public static void RemoveWeapon(WeaponItemType type)
	{
		EnsureData();
		_weaponInventoryData.Items[(int)type] = null;
		Save();
		OnInventoryChanged?.Invoke();
	}
}

public enum WeaponItemType
{
	MeleeWeapon = 0, 
	Pistol = 1, 
	HeavyWeapon = 2, 
	Item1 = 3, 
	Item2 = 4
}