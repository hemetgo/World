using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HemetTools.Inspector;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Transform _uiItemsParent;
    [SerializeField] InventoryItemUI _inventoryUIItemPrefab;

    [SerializeField] Dictionary<string, InventoryItemUI> _uiItems = new Dictionary<string, InventoryItemUI>();


	private void Start()
	{
        LoadItems();
	}

	private void OnEnable()
	{
		InventoryService.OnItemAdded += OnItemAdded;
		InventoryService.OnItemRemoved += OnItemRemoved;
		InventoryService.OnInventoryCleared += OnInventoryCleared;
	}

	private void OnDisable()
	{
		InventoryService.OnItemAdded -= OnItemAdded;
		InventoryService.OnItemRemoved -= OnItemRemoved;
		InventoryService.OnInventoryCleared -= OnInventoryCleared;
	}

	void OnInventoryCleared()
	{
		LoadItems();
	}

	void OnItemAdded(ItemSettings settings, ItemData item)
	{
		if (_uiItems.TryGetValue(settings.SaveID, out  InventoryItemUI uiItem))
		{
			uiItem.UpdateUI(item);
			return;
		}

		uiItem = Instantiate(_inventoryUIItemPrefab, _uiItemsParent);
		uiItem.UpdateUI(item);

		_uiItems.Add(settings.SaveID, uiItem);
	}

	void OnItemRemoved(ItemSettings settings, ItemData item)
	{
		if (_uiItems.TryGetValue(settings.SaveID, out InventoryItemUI uiItem))
		{
			if (item.Amount <= 0)
			{
				_uiItems.Remove(settings.SaveID);
				Destroy(uiItem.gameObject);
				return;
			}

			uiItem.UpdateUI(item);
			return;
		}
	}

	void LoadItems()
    {
		foreach(InventoryItemUI uiItem in _uiItems.Values)
		{
			Destroy(uiItem.gameObject);
		}

		_uiItems.Clear();

		foreach (ItemData item in InventoryService.GetItems().Values)
        {
            InventoryItemUI uiItem = Instantiate(_inventoryUIItemPrefab, _uiItemsParent);
            uiItem.UpdateUI(item);

			_uiItems.Add(item.SaveID, uiItem);
		}
    }
}
