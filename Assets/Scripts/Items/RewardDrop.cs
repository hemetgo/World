using UnityEngine;

[System.Serializable]
public class RewardDrop
{
    public ItemSettings Item;
	public int MinAmount = 1;
    public int MaxAmount = 1;
	[Range(1, 10)] public int Weight = 1;
}