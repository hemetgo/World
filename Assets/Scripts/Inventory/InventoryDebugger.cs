using UnityEngine;
using UnityEngine.UI;
using HemetTools.Inspector;

#if UNITY_EDITOR
public class InventoryDebugger : MonoBehaviour
{
	[SerializeField] 
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
}
#endif
