using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public Dictionary<string, ItemData> Items = new Dictionary<string, ItemData>();

	public InventoryData()
	{
		Items = new Dictionary<string, ItemData>();
	}

	public InventoryData(Dictionary<string, ItemData> items)
	{
		Items = items;
	}

	public ItemData AddItem(string itemID, int amount)
	{
		ItemData item = GetItem(itemID);
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
			Items.Remove(itemID);
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
		if (Items.TryGetValue(itemID, out ItemData itemData)) return itemData;

		ItemData item = new ItemData(itemID, 0);

		Items.Add(itemID, item);

		return item;
	}
}
