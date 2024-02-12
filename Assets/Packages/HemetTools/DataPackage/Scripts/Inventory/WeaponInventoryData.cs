using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;

public class WeaponInventoryData
{
    public Dictionary<int, ItemData> Items = new Dictionary<int, ItemData>();

    public WeaponInventoryData()
    {
        for(int i = 0; i < 5; i++)
        {
            Items[i] = null;
        }
    }

    public ItemData GetItem(WeaponItemType type)
    {
        if (Items.ContainsKey((int)type))
        {
            return Items[(int)type];
        }
        else
        {
            Items[(int)type] = null;
			return Items[(int)type];
		}
	}

	public void SetMeleeWeapon(ItemSettings itemSettings)
    {
		Items[0] = new ItemData(itemSettings.SaveID, 1);
    }

    public void SetPistol(ItemSettings itemSettings, int ammo)
	{
		Items[1] = new ItemData(itemSettings.SaveID, ammo);
	} 

    public void SetHeavyWeapon(ItemSettings itemSettings, int ammo)
    {
		Items[2] = new ItemData(itemSettings.SaveID, ammo);
    }

    public void SetItem(ItemSettings itemSettings, int amount)
    {
		Items[3] = new ItemData(itemSettings.SaveID, amount);
    }
}