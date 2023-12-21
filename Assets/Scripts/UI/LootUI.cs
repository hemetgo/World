using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootUI : MonoBehaviour
{
    [SerializeField] Transform _itemsParent;
    [SerializeField] LootItemUI _itemUIPrefab;

    List<LootItemUI> _itemsUI = new List<LootItemUI>();

    public void Setup(List<ItemSettings> items)
    {
        foreach (var item in items)
        {
            InstantiateItemUI(item);
		}
    }

    void InstantiateItemUI(ItemSettings itemSettings)
    {
        LootItemUI itemUI = Instantiate(_itemUIPrefab, _itemsParent);
        itemUI.Setup(itemSettings);

        _itemsUI.Add(itemUI);
    }
}
