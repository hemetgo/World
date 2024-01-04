using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
	[SerializeField] InventoryReferences inventoryReferences;
	[SerializeField] InventoryUI inventoryUI;

	private void Awake()
	{
		inventoryReferences.Initialize();
		inventoryUI.Initialize();
	}
}
