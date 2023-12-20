using UnityEngine;
using UnityEngine.UI;
using HemetTools.Inspector;

public class InventoryDebugger : MonoBehaviour
{
	[SerializeField] ItemSettings _item;
	[SerializeField] int _amount;

#if UNITY_EDITOR
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