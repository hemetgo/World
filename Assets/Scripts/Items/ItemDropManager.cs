using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropManager : MonoBehaviour
{
	[SerializeField] Terrain _terrain;
	[SerializeField] ItemDrop _dropPrefab;

    public static ItemDropManager Instance;

	private void Awake()
	{
		Instance = this;
	}

	public void ClaimDrop(List<RewardDrop> drops, Vector3 dropPosition)
	{
		RewardDrop drop = GetWeightedRandomDrop(drops);
		Drop(drop, dropPosition);
	}

	public void Drop(RewardDrop rewardDrop, Vector3 dropPosition)
	{
		ItemDrop drop = Instantiate(_dropPrefab, dropPosition, Quaternion.identity);
		drop.Setup(rewardDrop.Item, Random.Range(rewardDrop.MinAmount, rewardDrop.MaxAmount));

		drop.transform.Translate(Vector3.up);
	}

	public static RewardDrop GetWeightedRandomDrop(List<RewardDrop> drops)
	{
		int totalWeight = 0;

		foreach (var drop in drops)
		{
			totalWeight += drop.Weight;
		}

		int randomValue = Random.Range(1, totalWeight + 1);

		foreach (var drop in drops)
		{
			if (randomValue <= drop.Weight)
			{
				return drop;
			}

			randomValue -= drop.Weight;
		}

		// Fallback (não deveria acontecer, mas caso a soma dos pesos não seja igual a totalWeight)
		return drops[Random.Range(0, drops.Count)];
	}
}
