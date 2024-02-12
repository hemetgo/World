using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting.ReorderableList;

public class WeaponInventoryService
{
	public static WeaponItemCategories Categories;
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
		EnsureData();
		DataManager.Save(_weaponInventoryData);
	}

	public static ItemData GetItem(WeaponItemType type)
	{
		EnsureData();
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

	public static void SetItem(ItemSettings itemSettings, int amount)
	{
		EnsureData();
		_weaponInventoryData.SetItem(itemSettings, amount);
		Save();
		OnInventoryChanged?.Invoke();
	}

	public static void AddItem(ItemSettings itemSettings, int amount)
	{
		if (itemSettings == null) 
			return;

		EnsureData();
		
		if (itemSettings.Category == Categories.MeleeWeapon) SetMeleeWeapon(itemSettings);
		else if (itemSettings.Category == Categories.Pistol) SetPistol(itemSettings as FireWeaponSettings);
		else if (itemSettings.Category == Categories.HeavyWeapon) SetHeavyWeapon(itemSettings as FireWeaponSettings);
		else SetItem(itemSettings, amount);

		UnityEngine.Debug.Log($"{amount}x {itemSettings} has been added");

		Save();
		OnInventoryChanged?.Invoke();
	}

	public static void RemoveWeaponItem(WeaponItemType type)
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
	RangedWeapon = 1, 
}

[System.Serializable]
public struct WeaponItemCategories
{
	public ItemCategorySettings MeleeWeapon;
	public ItemCategorySettings Pistol;
	public ItemCategorySettings HeavyWeapon;
}