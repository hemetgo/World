using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string SaveID;
    public int Amount;

	public ItemData(string saveID, int amount)
	{
		SaveID = saveID;
		Amount = amount;
	}

	public void Add(int amount)
	{
		Amount += amount;
	}

	public void Remove(int amount)
	{
		Amount -= amount;
	}
}
