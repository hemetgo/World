using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HemetTools.Inspector;

public class LootItemUI : MonoBehaviour
{
    [SerializeField, ReadOnly] ItemSettings _itemSettings;
    [SerializeField] TextMeshProUGUI _itemNameText;
    [SerializeField] Button _getItemButton;

	private void OnEnable()
	{
		_getItemButton.onClick.AddListener(GetItem);
	}

	private void OnDisable()
	{
		_getItemButton.onClick.RemoveListener(GetItem);
	}

	public void Setup(ItemSettings itemSettings)
    {
		_itemSettings = itemSettings;
		_itemNameText.text = _itemSettings.Name;
	}

    void GetItem()
    {
		InventoryService.AddItem(_itemSettings, 1);
		DestroyImmediate(gameObject);
    }
}
