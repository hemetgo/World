using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drop", menuName = "Survival/Item/Drop")]
public class DropSettings : ScriptableObject
{
    [Range(0, 1)] public float DropChance;
    public List<DropGroups> Drops = new List<DropGroups>();

	public DropGroups GetWeightedRandomRarityDrop()
	{
		int totalWeight = 0;

		foreach (var drop in Drops)
		{
			if (!drop.Enabled) continue;

			totalWeight += drop.Weight;
		}

		int randomValue = Random.Range(1, totalWeight + 1);

		foreach (var drop in Drops)
		{
			if (!drop.Enabled) continue;

			if (randomValue <= drop.Weight)
			{
				return drop;
			}

			randomValue -= drop.Weight;
		}

		return Drops[Random.Range(0, Drops.Count)];
	}
}


public enum Rarity
{
    Common, Uncommon, Rare, Legendary
}