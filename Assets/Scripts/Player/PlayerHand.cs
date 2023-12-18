using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
	[SerializeField] HandItem _currentItem;
    [SerializeField] List<HandItem> _items = new List<HandItem>();

	PlayerController _controller;

	private void Awake()
	{
		_controller = GetComponent<PlayerController>();
	}

	private void Update()
	{
		if (_controller.IsCollecting)
		{
			ActivateItem(_controller.ItemCollector.GetRequiredCollectingTool());
		}
		else if (_controller.IsShooting)
		{
			ActivateItem(_controller.Combat.CurrentWeaponSettings);
		}
		else
		{
			DeactivateAllItems();
		}
	}

	public HandWeapon GetWeapon(WeaponSettings settings)
	{
		foreach (HandItem item in _items)
		{
			if (item.ItemSettings == settings)
			{
				return item as HandWeapon;
			}
		}

		return null;
	}

	public void DeactivateAllItems()
	{
		_items.ForEach(item => item.gameObject.SetActive(false));
		_currentItem = null;
	}

	public void ActivateItem(ItemSettings itemSettings)
	{
		if (itemSettings == null) return;

		foreach (HandItem item in _items)
		{
			if (item.ItemSettings == itemSettings)
			{
				item.gameObject.SetActive(true);
				_currentItem = item;
			}
			else
			{
				item.gameObject.SetActive(false);
			}
		}
	}
}