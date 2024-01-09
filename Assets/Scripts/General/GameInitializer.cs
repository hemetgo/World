using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
	[SerializeField] InventoryReferences inventoryReferences;
	[SerializeField] InventoryUI inventoryUI;
	[SerializeField] PlayerController player;

	private void Awake()
	{
		InventoryService.ResetInventory();
		CurrencyService.ResetCurrencies();

		inventoryReferences.Initialize();
		//inventoryUI.Initialize();
		player.Initialize();
	}
}
