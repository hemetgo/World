using HemetTools.Inspector;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class PlayerHand : MonoBehaviour
{
	[SerializeField] List<ItemSettings> _initialItems = new List<ItemSettings>();
	[SerializeField] Transform _itemsContainer;
	
	List<HandItem> _handItemReferences = new List<HandItem>();

	List<ItemData> InventoryItems => InventoryService.GetItems();

	public static int CurrentHandItemIndex;
	public HandItem CurrentHandItem
	{
		get
		{
			if (InventoryItems.Count == 0)
				return null;

			foreach(HandItem handItem in _handItemReferences)
			{
				if (handItem.ItemSettings.SaveID == InventoryItems[CurrentHandItemIndex].SaveID)
				{
					return handItem;
				}
			}

			return null;
		}
	}

	private void OnEnable()
	{
		GameEvents.Inputs.OnScrollUp += NextItem;
		GameEvents.Inputs.OnScrollDown += PreviousItem;
		InventoryService.OnInventoryChanged += ItemIndex;
	}

	private void OnDisable()
	{
		GameEvents.Inputs.OnScrollUp -= NextItem;
		GameEvents.Inputs.OnScrollDown -= PreviousItem;
		InventoryService.OnInventoryChanged -= ItemIndex;
	}

	private void OnDestroy()
	{
		OnDisable();
	}

	public void Initialize()
	{
		_initialItems.ForEach(item => InventoryService.AddItem(item, 1));
		_handItemReferences = new List<HandItem>(_itemsContainer.GetComponentsInChildren<HandItem>(true));

		CurrentHandItemIndex = 0;
	}

	private void Start()
	{
		UpdateHand();
	}

	public void Evaluate() { }

	void ItemIndex()
	{
		if (CurrentHandItemIndex >= InventoryItems.Count)
		{
			CurrentHandItemIndex = InventoryItems.Count - 1;
		}

		UpdateHand();
	}

	public bool IsHolding(ItemCategorySettings categorySettings)
	{
		if (CurrentHandItem == null) 
			return false;

		if (CurrentHandItem.ItemSettings.Category == categorySettings)
		{
			return true;
		}

		return false;
	}

	public bool IsHolding(List<ItemCategorySettings> categoriesSettings)
	{
		foreach(ItemCategorySettings categorySettings in categoriesSettings)
		{
			if (IsHolding(categorySettings))
			{
				return true;
			}
		}

		return false;
	}

	public void UpdateHand()
	{
		HandItem currentItem = null;

		foreach (HandItem item in _handItemReferences)
		{
			if (CurrentHandItem == null)
				return;

			if (item.ItemSettings == CurrentHandItem.ItemSettings)
			{
				currentItem = item;
				continue;
			}

			item.gameObject.SetActive(false);
			item.OnUnequip();
		}

		if (currentItem)
		{
			currentItem.gameObject.SetActive(true);
			currentItem.OnEquip();
		}
	}

	public void SwitchCurrentItem(ItemSettings newItem, int newAmount)
	{
		ItemSettings currentItemSettings = CurrentHandItem.ItemSettings;
		int amount = InventoryItems[CurrentHandItemIndex].Amount;

		InventoryService.SwitchItem(newItem, newAmount, CurrentHandItemIndex);
		ItemDropManager.Instance.Drop(currentItemSettings, amount, transform.position, true);

		UpdateHand();
	}

	public void SelectItem(ItemSettings itemSettings)
	{
		List<ItemData> list = InventoryService.GetItems();
		for (int i = 0; i < list.Count; i++)
		{
			ItemData item = list[i];
			if (item.SaveID.Equals(itemSettings.SaveID))
			{
				CurrentHandItemIndex = i;
				UpdateHand();
				return;
			}
		}
	}

	public void NextItem()
	{
		CurrentHandItemIndex++;
		if (CurrentHandItemIndex >= InventoryService.GetItems().Count)
		{
			CurrentHandItemIndex = 0;
		}

		UpdateHand();
	}

	public void PreviousItem()
	{
		CurrentHandItemIndex--;
		if (CurrentHandItemIndex < 0)
		{
			CurrentHandItemIndex = InventoryService.GetItems().Count - 1;
		}

		UpdateHand();
	}

	
	/// <summary>
	/// Is called on shooting animation
	/// </summary>
	public void UseHandItem()
	{
		CurrentHandItem.OnUse();
	}
}