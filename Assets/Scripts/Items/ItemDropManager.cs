using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class ItemDropManager : MonoBehaviour
{
	[SerializeField] Terrain _terrain;
	[SerializeField] ItemDrop _dropPrefab;

    public static ItemDropManager Instance;

	private void Awake()
	{
		Instance = this;
	}

	public void InstantiateAirDrop(List<DropElement> drops)
	{
		Drop(drops, GetRandomPositionOnTerrain() + Vector3.up * 7.5f);
	}

	public void Drop(List<DropElement> drops, Vector3 dropPosition)
	{
		DropElement drop = GetWeightedRandomDrop(drops);
		Drop(drop, dropPosition);
	}

	public void Drop(DropElement rewardDrop, Vector3 dropPosition)
	{
		Drop(rewardDrop.Item, Random.Range(rewardDrop.MinAmount, rewardDrop.MaxAmount + 1), dropPosition);
	}

	public void Drop(ItemSettings item, int amount, Vector3 dropPosition, bool applyForce = false)
	{
		ItemDrop drop = Instantiate(_dropPrefab, dropPosition, Quaternion.identity);
		drop.Setup(item, amount);

		drop.transform.Translate(Vector3.up);
		drop.transform.SetParent(transform);

		if (applyForce) drop.ApplyForce();
	}

	public static DropElement GetWeightedRandomDrop(List<DropElement> drops)
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

	Vector3 GetRandomPositionOnTerrain()
	{
		TerrainData terrainData = _terrain.terrainData;
		Vector3 terrainSize = terrainData.size;

		float randomX = Random.Range(0f, terrainSize.x);
		float randomZ = Random.Range(0f, terrainSize.z);

		// Obtém a altura correspondente na posição aleatória
		float randomY = _terrain.SampleHeight(new Vector3(randomX, 0f, randomZ));

		// Combina as coordenadas para obter a posição final
		Vector3 randomTerrainPosition = new Vector3(randomX, randomY, randomZ);

		return _terrain.transform.position + randomTerrainPosition;
	}
}