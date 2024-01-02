using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
	public List<ItemData> Items = new List<ItemData>();
	public int InventorySize;

	public bool IsFull => Items.Count >= InventorySize;

	public InventoryData()
	{
		Items = new List<ItemData>();
		InventorySize = 10;
	}

	public InventoryData(List<ItemData> items, int inventorySize)
	{
		Items = items;
		InventorySize = inventorySize;
	}

	public ItemData AddItem(string itemID, bool cumulative, int amount)
	{
		if (Items.Count >= InventorySize)
		{
			if (!cumulative)
			{
				Debug.LogError($"Inventory is full. Current inventory size is {InventorySize}");
				return null;
			}
		}

		ItemData item = GetItem(itemID, cumulative);
		item.Add(amount);

		Debug.Log($"{amount}x {itemID} has been added");

		return item;
	}

	public ItemData RemoveItem(string itemID, int amount)
	{
		ItemData item = GetItem(itemID);
		item.Remove(amount);
		if (GetItem(itemID).Amount <= 0)
		{
			Items.Remove(item);
		}

		Debug.Log($"{amount}x {itemID} has been removed");

		return item;
	}

	public void Clear()
	{
		Items.Clear();

		Debug.Log("Inventory has been cleared");
	}

	/// <summary>
	/// Return an item with specified name, if it isn't exists, create one 
	/// </summary>
	private ItemData GetItem(string itemID)
	{
		if (TryGetItem(itemID, out ItemData item) == true)
		{
			return item;
		}

		ItemData newItem = new ItemData(itemID, 0);

		Items.Add(newItem);

		return newItem;
	}

	/// <summary>
	/// Return an item with specified name, if it isn't exists, create one 
	/// </summary>
	private ItemData GetItem(string itemID, bool cumulative)
	{
		if (cumulative)
		{
			if (TryGetItem(itemID, out ItemData item) == true)
			{
				return item;
			}
		}

		ItemData newItem = new ItemData(itemID, 0);

		Items.Add(newItem);

		return newItem;
	}

	public bool TryGetItem(string itemID, out ItemData item)
	{
		foreach (ItemData filteredItem in Items)
		{
			if (filteredItem.SaveID == itemID)
			{
				item = filteredItem;
				return true;
			}
		}

		item = null;
		return false;
	}
}