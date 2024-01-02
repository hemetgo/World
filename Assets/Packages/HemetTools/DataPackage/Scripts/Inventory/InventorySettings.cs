using HemetTools.Inspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySettings : ScriptableObject
{
	public string InventoryID;

#if UNITY_EDITOR
	[SerializeField] ItemSettings _item;
	[SerializeField] int _amount;

	[Button]
	public void AddItem()
	{
		InventoryService.AddItem(_item, _amount);
	}

	[Button]
	public void RemoveItem()
	{
		InventoryService.RemoveItem(_item, _amount);
	}

	[Button]
	public void ClearInventory()
	{
		InventoryService.ClearInventory();
	}
#endif
}
