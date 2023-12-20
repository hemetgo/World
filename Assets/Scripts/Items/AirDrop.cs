using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDrop : MonoBehaviour
{
    [SerializeField] DropItem[] _drops;

    public void ClaimDrop()
    {
        foreach (var item in _drops)
        {
            InventoryService.AddItem(item.Item, item.Amount);
        }
    }
}

[System.Serializable]
public class DropItem
{
    public ItemSettings Item;
    public int Amount;
}