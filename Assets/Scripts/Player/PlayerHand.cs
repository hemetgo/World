using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
	[SerializeField] HandItem _currentItem;
    [SerializeField] List<HandItem> _items = new List<HandItem>();

	public void DeactivateAllItems()
	{
		_items.ForEach(item => item.gameObject.SetActive(false));
		_currentItem = null;
	}

	public void ActivateItem(ItemSettings itemSettings)
	{
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