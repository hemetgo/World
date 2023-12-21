using System.Collections.Generic;
using System.Net;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
	[SerializeField] List<ItemSettings> _initialItems = new List<ItemSettings>();
	
	[SerializeField] List<HandItem> _handItems = new List<HandItem>();

	List<ItemData> InventoryItems => InventoryService.GetItems();

	public static int CurrentHandItemIndex;
	public HandItem CurrentHandItem
	{
		get
		{
			if (InventoryItems.Count == 0)
				return null;

			foreach(HandItem handItem in _handItems)
			{
				if (handItem.ItemSettings.SaveID == InventoryItems[CurrentHandItemIndex].SaveID)
				{
					return handItem;
				}
			}

			return null;
		}
	}

	private void Awake()
	{
		InventoryService.ClearInventory();
		_initialItems.ForEach(item => InventoryService.AddItem(item, 1));
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

	public HandWeapon GetWeapon(WeaponSettings settings)
	{
		foreach (HandItem item in _handItems)
		{
			if (item.ItemSettings == settings)
			{
				return item as HandWeapon;
			}
		}

		return null;
	}

	public void DeactivateAllItems()
	{
		_handItems.ForEach(item => item.gameObject.SetActive(false));
	}

	public void UpdateHand()
	{
		foreach (HandItem item in _handItems)
		{
			item.gameObject.SetActive(item.ItemSettings == CurrentHandItem.ItemSettings);
		}
	}

	public void NextItem()
	{
		CurrentHandItemIndex++;
		if (CurrentHandItemIndex >= InventoryService.GetItems().Count)
		{
			CurrentHandItemIndex = 0;
		}

		UpdateHand();
	}
}