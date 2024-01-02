using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEditor;

public class InventoryService 
{
    private static InventoryData inventoryData;
	
	public static Action<ItemSettings, ItemData> OnItemAdded;
	public static Action<ItemSettings, ItemData> OnItemRemoved;
	public static Action OnInventoryCleared;
	public static Action OnInventoryChanged;

	//public static bool IsFull => inventoryData.IsFull;

	public static ItemData GetItem(string inventoryId, string saveId)
	{
		EnsureData();

		inventoryData.TryGetItem(inventoryId, saveId, out ItemData item);

		return item;
	}

	public static bool TryGetItem(string inventoryId, string saveId, out ItemData item)
	{
		inventoryData.TryGetItem(inventoryId,saveId, out ItemData resultItem);

		item = resultItem;
		return item != null;
	}

	public static List<ItemData> GetItems(string inventoryId)
	{
		EnsureData();

		return inventoryData.Inventories[inventoryId];
	}

	public static void AddItem(string inventoryId, ItemSettings settings, int amount)
	{
		if (amount < 1) Debug.LogWarning($"The sent amount value is {amount} and it cannot be less than 1");

		EnsureData();

		ItemData item = inventoryData.AddItem(inventoryId, settings.SaveID, settings.Cumulative, amount);
		Save();

		if (item == null)
			return;

		OnItemAdded?.Invoke(settings, item);
		OnInventoryChanged?.Invoke();
	}

	public static void RemoveItem(string inventoryId, ItemSettings settings, int amount) 
	{
		if (amount < 1) Debug.LogWarning($"The sent amount value is {amount} and it cannot be less than 1"); 

		EnsureData();

		ItemData item = inventoryData.RemoveItem(inventoryId, settings.SaveID, amount);
		Save();

		OnItemRemoved?.Invoke(settings, item);
		OnInventoryChanged?.Invoke();
	}

#if UNITY_EDITOR
	[MenuItem("HemetTools/Inventory/Clear All Inventories")]
#endif
	public static void ClearAllInventories()
	{
		EnsureData();

		inventoryData.Clear();
		Save();

		OnInventoryCleared?.Invoke();
		OnInventoryChanged?.Invoke();
	}

	public static void ClearInventory(string inventoryId)
	{
		EnsureData();

		inventoryData.Inventories[inventoryId].Clear();
		Save();

		OnInventoryCleared?.Invoke();
		OnInventoryChanged?.Invoke();
	}

	private static void Save()
	{
		DataManager.Save(inventoryData);
	}

	private static void EnsureData()
	{
		if (inventoryData == null)
		{
			inventoryData = DataManager.Load<InventoryData>();
		}
	}
}