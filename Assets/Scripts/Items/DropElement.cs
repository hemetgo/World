using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropElement
{
    public ItemSettings Item;
	public int MinAmount = 1;
    public int MaxAmount = 1;
	[Range(1, 10)] public int Weight = 1;
}

[System.Serializable]
public class DropGroups
{
	public string Name;
	public bool Enabled;
	public int Weight;
	public List<DropElement> Elements = new List<DropElement>();
}