using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
	[SerializeField] Transform _itemsContainer;
	[SerializeField] InitialWeaponInventory _initialItems;
	
	List<HandItem> _handItemReferences = new List<HandItem>();

	public static WeaponItemType CurrentItemType { get; private set; }
	public static ItemData CurrentItemData => WeaponInventoryService.GetItem(CurrentItemType);

	public HandItem CurrentHandItem 
	{
		get
		{
            foreach (var item in _handItemReferences)
            {
                if (item.ItemSettings.SaveID == CurrentItemData.SaveID)
				{
					return item;
				}
            }
            return null;
		} 
	}

	private void OnEnable()
	{
		GameEvents.Inputs.OnScrollUp += NextItem;
		GameEvents.Inputs.OnScrollDown += PreviousItem;
		WeaponInventoryService.OnInventoryChanged += OnInventoryChanged;
	}

	private void OnDisable()
	{
		GameEvents.Inputs.OnScrollUp -= NextItem;
		GameEvents.Inputs.OnScrollDown -= PreviousItem;
		WeaponInventoryService.OnInventoryChanged -= OnInventoryChanged;
	}

	private void OnDestroy()
	{
		OnDisable();
	}

	public void Initialize()
	{
		_initialItems.Initialize();
		_handItemReferences = new List<HandItem>(_itemsContainer.GetComponentsInChildren<HandItem>(true));

		CurrentItemType = WeaponItemType.Pistol;
	}

	private void Start()
	{
		UpdateHand();
	}

	public void Evaluate() { }

	void OnInventoryChanged()
	{
		if (CurrentItemData == null)
		{
			if (CurrentItemType == WeaponItemType.MeleeWeapon)
				NextItem();
			else
				PreviousItem();
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
		//ItemSettings currentItemSettings = CurrentHandItem.ItemSettings;
		//int amount = InventoryItems[CurrentHandItemIndex].Amount;

		//InventoryService.SwitchItem(newItem, newAmount, CurrentHandItemIndex);
		//ItemDropManager.Instance.Drop(currentItemSettings, amount, transform.position, true);

		//UpdateHand();
	}

	public void SelectItem(ItemSettings itemSettings)
	{
		List<ItemData> list = InventoryService.GetItems();
		for (int i = 0; i < list.Count; i++)
		{
			ItemData item = list[i];
			if (item.SaveID.Equals(itemSettings.SaveID))
			{
				//CurrentHandItemIndex = i;
				UpdateHand();
				return;
			}
		}
	}

	WeaponItemType GetNextWeaponItemType(WeaponItemType current)
	{
		List<ItemData> items = new List<ItemData>(WeaponInventoryService.Items.Values.ToList());

		int next = (int)current + 1;
		if (next >= items.Count)
		{
			next = 0;
		}

		for (int i = next; i < items.Count; i++)
        {
			ItemData item = items[i];

			if (item != null)
			{
				next = i;
				return (WeaponItemType)next;
			}
        }

		return current;
    }

	WeaponItemType GetPreviousWeaponItemType(WeaponItemType current)
	{
		List<ItemData> items = new List<ItemData>(WeaponInventoryService.Items.Values.ToList());

		int next = (int)current - 1;
		if (next < 0)
		{
			next = items.Count - 1;
		}

		for (int i = next; i < items.Count; i--)
		{
			ItemData item = items[i];

			if (item != null)
			{
				next = i;
				return (WeaponItemType)next;
			}
		}

		return current;
	}

	public void NextItem()
	{
		CurrentItemType = GetNextWeaponItemType(CurrentItemType);
		UpdateHand();
	}

	public void PreviousItem()
	{
		CurrentItemType = GetPreviousWeaponItemType(CurrentItemType);
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