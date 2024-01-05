using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HemetTools.Inspector;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Transform _uiItemsParent;
    [SerializeField] InventoryItemUI _inventoryUIItemPrefab;

    [SerializeField] List<InventoryItemUI> _uiItems = new List<InventoryItemUI>();


	public void Initialize()
	{
        LoadItemsOnUI();
	}

	private void OnEnable()
	{
		InventoryService.OnItemAdded += OnItemAdded;
		InventoryService.OnItemRemoved += OnItemRemoved;
		InventoryService.OnInventoryCleared += OnInventoryCleared;

		GameEvents.Player.EquipItem += OnItemEquipped;
	}

	private void OnDisable()
	{
		InventoryService.OnItemAdded -= OnItemAdded;
		InventoryService.OnItemRemoved -= OnItemRemoved;
		InventoryService.OnInventoryCleared -= OnInventoryCleared;

		GameEvents.Player.EquipItem += OnItemEquipped;
	}

	void OnItemEquipped(HandItem handItem)
	{
		for (int i = 0; i < _uiItems.Count; i++)
		{
			InventoryItemUI item = _uiItems[i];
			item.Select(i == PlayerHand.CurrentHandItemIndex);
		}
	}

	void OnInventoryCleared()
	{
		LoadItemsOnUI();
	}

	void OnItemAdded(ItemSettings settings, ItemData item)
	{
		if (settings.Cumulative && TryGetItemUI(settings.SaveID, out InventoryItemUI uiItem))
		{
			uiItem.UpdateUI(item);
			return;
		}

		uiItem = Instantiate(_inventoryUIItemPrefab, _uiItemsParent);
		uiItem.UpdateUI(item);

		_uiItems.Add(uiItem);
	}

	void OnItemRemoved(ItemSettings settings, ItemData item)
	{
		if (TryGetItemUI(settings.SaveID, out InventoryItemUI uiItem))
		{
			if (item.Amount <= 0)
			{
				_uiItems.Remove(uiItem);
				Destroy(uiItem.gameObject);
				return;
			}

			uiItem.UpdateUI(item);
			return;
		}
	}

	void LoadItemsOnUI()
    {
		foreach(InventoryItemUI uiItem in _uiItems)
		{
			Destroy(uiItem.gameObject);
		}

		_uiItems.Clear();

		foreach (ItemData item in InventoryService.GetItems())
        {
            InventoryItemUI uiItem = Instantiate(_inventoryUIItemPrefab, _uiItemsParent);
            uiItem.UpdateUI(item);

			_uiItems.Add(uiItem);
		}
    }

	bool TryGetItemUI(string saveID, out InventoryItemUI itemUI)
	{
        foreach (InventoryItemUI inventoryItem in _uiItems)
        {
            if(inventoryItem.ItemData.SaveID == saveID)
			{
				itemUI = inventoryItem;
				return true;
			}
        }

		itemUI = null;
		return false;
    }
}
