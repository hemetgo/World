
using System.Collections.Generic;

[System.Serializable]
public class InitialWeaponInventory
{
    public List<InitialItem> InitialItems = new List<InitialItem>();

    public void Initialize()
    {
        InitialItems.ForEach(item => WeaponInventoryService.AddItem(item.Item, item.Amount));
    }
}

[System.Serializable]
public class InitialItem
{
    public ItemSettings Item;
    public int Amount;
}
