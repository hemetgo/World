using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public Dictionary<string, List<ItemData>> Inventories = new Dictionary<string, List<ItemData>>();

	//public bool IsFull => Items.Count >= InventorySize;

	public InventoryData()
	{
		Inventories = new Dictionary<string, List<ItemData>>();
	}

	public ItemData AddItem(string inventoryId, string itemID, bool cumulative, int amount)
	{
		//if (Items.Count >= InventorySize)
		//{
		//	if (!cumulative)
		//	{
		//		Debug.LogError($"Inventory is full. Current inventory size is {InventorySize}");
		//		return null;
		//	}
		//}

		ItemData item = GetItem(inventoryId, itemID, cumulative);
		item.Add(amount);

		Debug.Log($"{amount}x {itemID} has been added");

		return item;
	}

	public ItemData RemoveItem(string inventoryId, string itemID, int amount)
	{
		ItemData item = GetItem(inventoryId, itemID);
		item.Remove(amount);
		if (GetItem(inventoryId, itemID).Amount <= 0)
		{
			Inventories[inventoryId].Remove(item);
		}

		Debug.Log($"{amount}x {itemID} has been removed");

		return item;
	}

	public void Clear()
	{
		Inventories.Clear();

		Debug.Log("Inventory has been cleared");
	}

	/// <summary>
	/// Return an item with specified name, if it isn't exists, create one 
	/// </summary>
	private ItemData GetItem(string inventoryId, string itemID)
	{
		if (TryGetItem(inventoryId, itemID, out ItemData item) == true)
		{
			return item;
		}

		ItemData newItem = new ItemData(itemID, 0);

		Inventories[inventoryId].Add(newItem);

		return newItem;
	}

	/// <summary>
	/// Return an item with specified name, if it isn't exists, create one 
	/// </summary>
	private ItemData GetItem(string inventoryId, string itemID, bool cumulative)
	{
		if (cumulative)
		{
			if (TryGetItem(inventoryId, itemID, out ItemData item) == true)
			{
				return item;
			}
		}

		ItemData newItem = new ItemData(itemID, 0);

		Inventories[inventoryId].Add(newItem);

		return newItem;
	}

	public bool TryGetItem(string inventoryId, string itemID, out ItemData item)
	{
		foreach (ItemData filteredItem in Inventories[inventoryId])
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
