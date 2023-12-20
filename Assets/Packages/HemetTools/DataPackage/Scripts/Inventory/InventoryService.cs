using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEditor;

public class InventoryService 
{
    private static InventoryData inventory;
	
	public static Action<ItemSettings, ItemData> OnItemAdded;
	public static Action<ItemSettings, ItemData> OnItemRemoved;
	public static Action OnInventoryCleared;
	public static Action OnInventoryChanged;

	public static ItemData GetItem(string saveId)
	{
		EnsureData();

		if (!inventory.Items.ContainsKey(saveId)) return null;

		return inventory.Items[saveId];
	}

	public static Dictionary<string, ItemData> GetItems()
	{
		EnsureData();

		return inventory.Items;
	}

	public static void AddItem(ItemSettings settings, int amount)
	{
		if (amount < 1) Debug.LogWarning($"The sent amount value is {amount} and it cannot be less than 1");

		EnsureData();

		ItemData item = inventory.AddItem(settings.SaveID, amount);
		Save();

		OnItemAdded?.Invoke(settings, item);
		OnInventoryChanged?.Invoke();
	}

	public static void RemoveItem(ItemSettings settings, int amount) 
	{
		if (amount < 1) Debug.LogWarning($"The sent amount value is {amount} and it cannot be less than 1"); 

		EnsureData();

		ItemData item = inventory.RemoveItem(settings.SaveID, amount);
		Save();

		OnItemRemoved?.Invoke(settings, item);
		OnInventoryChanged?.Invoke();
	}

#if UNITY_EDITOR
	[MenuItem("HemetTools/Inventory/Clear Inventory")]
#endif
	public static void ClearInventory()
	{
		EnsureData();

		inventory.Clear();
		Save();

		OnInventoryCleared?.Invoke();
		OnInventoryChanged?.Invoke();
	}

	private static void Save()
	{
		DataManager.Save(inventory);
	}

	private static void EnsureData()
	{
		if (inventory == null)
		{
			inventory = DataManager.Load<InventoryData>();
		}
	}
}