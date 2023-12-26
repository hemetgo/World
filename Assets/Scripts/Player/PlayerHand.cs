using System.Collections.Generic;
using System.Net;
using TMPro;
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

	private void Start()
	{
		UpdateHand();
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

	public void UpdateHand()
	{
		foreach (HandItem item in _handItems)
		{
			if (CurrentHandItem == null)
				return;

			if (item.ItemSettings == CurrentHandItem.ItemSettings)
			{
				item.gameObject.SetActive(true);
				item.OnActivated();
				return;
			}
			
			item.gameObject.SetActive(false);
			item.OnDeactivated();
		}
	}

	public void SelectItem(ItemSettings itemSettings)
	{
		List<ItemData> list = InventoryService.GetItems();
		for (int i = 0; i < list.Count; i++)
		{
			ItemData item = list[i];
			if (item.SaveID.Equals(itemSettings.SaveID))
			{
				CurrentHandItemIndex = i;
				UpdateHand();
				return;
			}
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