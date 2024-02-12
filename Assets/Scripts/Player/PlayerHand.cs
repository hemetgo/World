using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
	[SerializeField] Transform _itemsContainer;
	
	Dictionary<string, HandItem> _handItemReferences = new Dictionary<string, HandItem>();

	[SerializeField] HandMeleeWeapon _meleeWeapon;
	[SerializeField] HandFireWeapon _rangedWeapon;

	public static WeaponItemType CurrentItemType { get; private set; }

	bool _initialized;

	public HandItem CurrentHandItem => CurrentItemType == WeaponItemType.MeleeWeapon ? _meleeWeapon : _rangedWeapon;

	private void OnEnable()
	{
		GameEvents.Inputs.OnScrollUp += SwitchItem;
		GameEvents.Inputs.OnScrollDown += SwitchItem;
	}

	private void OnDisable()
	{
		GameEvents.Inputs.OnScrollUp -= SwitchItem;
		GameEvents.Inputs.OnScrollDown -= SwitchItem;
	}

	private void OnDestroy()
	{
		OnDisable();
	}

	public void Initialize()
	{
		if (!_initialized) 
			return;

		_initialized = true;
		
		_handItemReferences.Clear();
		foreach (HandItem handItem in _itemsContainer.GetComponentsInChildren<HandItem>(true))
		{
			_handItemReferences[handItem.ItemSettings.SaveID] = handItem;
		}

		CurrentItemType = WeaponItemType.RangedWeapon;
		SwitchItem();
	}

	private void Start()
	{
		UpdateHand();
	}

	public void Evaluate() { }

	HandItem GetHandItem(string saveID)
	{
		if (_handItemReferences.TryGetValue(saveID, out var Item))
		{
			return Item;
		}

		return null;
	}

	public bool IsHolding(ItemCategorySettings categorySettings)
	{
		if (CurrentHandItem == null) 
			return false;

		if (CurrentHandItem.ItemSettings.Category == categorySettings)
		{
			return true;
		}

		return false;
	}

	public bool IsHolding(List<ItemCategorySettings> categoriesSettings)
	{
		foreach(ItemCategorySettings categorySettings in categoriesSettings)
		{
			if (IsHolding(categorySettings))
			{
				return true;
			}
		}

		return false;
	}

	public void UpdateHand()
	{
		HandItem currentItem = null;

		foreach (HandItem item in _handItemReferences.Values)
		{
			if (CurrentHandItem == null)
				return;

			if (item.ItemSettings == CurrentHandItem.ItemSettings)
			{
				currentItem = item;
				continue;
			}

			item.gameObject.SetActive(false);
			item.OnUnequip();
		}

		if (currentItem)
		{
			currentItem.gameObject.SetActive(true);
			currentItem.OnEquip();
		}
	}

	public void SwitchCurrentItem(ItemSettings newItem, int newAmount)
	{
		//ItemSettings currentItemSettings = CurrentHandItem.ItemSettings;
		//int amount = InventoryItems[CurrentHandItemIndex].Amount;

		//InventoryService.SwitchItem(newItem, newAmount, CurrentHandItemIndex);
		//ItemDropManager.Instance.Drop(currentItemSettings, amount, transform.position, true);

		//UpdateHand();
	}

	public void SelectItem(ItemSettings itemSettings)
	{
		List<ItemData> list = InventoryService.GetItems();
		for (int i = 0; i < list.Count; i++)
		{
			ItemData item = list[i];
			if (item.SaveID.Equals(itemSettings.SaveID))
			{
				//CurrentHandItemIndex = i;
				UpdateHand();
				return;
			}
		}
	}

	public void SwitchItem()
	{
		CurrentItemType = CurrentItemType == WeaponItemType.MeleeWeapon ? WeaponItemType.RangedWeapon : WeaponItemType.MeleeWeapon;
		UpdateHand();
	}
	
	/// <summary>
	/// Is called on shooting animation
	/// </summary>
	public void UseHandItem()
	{
		CurrentHandItem.Use();
	}
}