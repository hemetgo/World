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
	public static Action<ItemSettings, ItemData, int> OnItemSwitched;
	public static Action OnInventoryCleared;
	public static Action OnInventoryChanged;

	public static bool IsFull => inventory.IsFull;

	public static ItemData GetItem(string saveId)
	{
		EnsureData();

		inventory.TryGetItem(saveId, out ItemData item);

		return item;
	}

	public static ItemData GetItem(int inventoryIndex)
	{
		EnsureData();

		return inventory.GetItem(inventoryIndex);
	}

	public static bool TryGetItem(string saveId, out ItemData item)
	{
		inventory.TryGetItem(saveId, out ItemData resultItem);

		item = resultItem;
		return item != null;
	}

	public static List<ItemData> GetItems()
	{
		EnsureData();

		return inventory.Items;
	}

	public static void AddItem(ItemSettings settings, int amount)
	{
		if (amount < 1) Debug.LogWarning($"The sent amount value is {amount} and it cannot be less than 1");

		EnsureData();

		ItemData item = inventory.AddItem(settings.SaveID, settings.Cumulative, amount);
		Save();

		if (item == null)
			return;

		OnItemAdded?.Invoke(settings, item);
		OnInventoryChanged?.Invoke();
	}

	public static void RemoveItem(ItemSettings settings)
	{
		EnsureData();

		RemoveItem(settings, GetItem(settings.SaveID).Amount);
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

	public static void SwitchItem(ItemSettings itemSettings, int amount, int itemIndex)
	{
		EnsureData();

		if (inventory.Items.Count < itemIndex)
		{
			Debug.LogError("Failed on item switch: Index out of range");
			return;
		}

		inventory.Items[itemIndex] = new ItemData(itemSettings.SaveID, amount);

		OnItemSwitched?.Invoke(itemSettings, inventory.Items[itemIndex], itemIndex);
	}

#if UNITY_EDITOR
	[MenuItem("HemetTools/Inventory/Clear Inventory")]
#endif
	public static void ResetInventory()
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