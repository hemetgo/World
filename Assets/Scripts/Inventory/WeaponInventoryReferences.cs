using UnityEngine;

public class WeaponInventoryReferences : InventoryReferences
{
	[SerializeField] InitialWeaponInventory _initialItems;
	[SerializeField] WeaponItemCategories _itemCategories;

	public override void Initialize()
	{
		if (_initialized) return;

		WeaponInventoryService.Categories = _itemCategories;

		base.Initialize();

		_initialItems.Initialize();
	}
}
