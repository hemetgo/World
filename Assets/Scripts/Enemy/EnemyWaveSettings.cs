using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Settings", menuName = "Survival/Enemy/Wave Settings")]
public class EnemyWaveSettings : ScriptableObject
{
	public List<EnemySpawn> EnemiesPrefabs = new List<EnemySpawn>();
	public GameObject EnemySpawnVFX;
	public float SpawnRange;

	public int[] EnemiesToSpawn;

	public float WaveWaitTime;
	public float PrepareWaveTime;

	public EnemySpawn GetRandomEnemySpawn()
	{
		int totalWeight = 0;

		foreach (var spawn in EnemiesPrefabs)
		{
			totalWeight += spawn.Weight;
		}

		int randomValue = UnityEngine.Random.Range(0, totalWeight);

		foreach (var spawn in EnemiesPrefabs)
		{
			if (randomValue < spawn.Weight)
			{
				return spawn;
			}

			randomValue -= spawn.Weight;
		}

		throw new Exception("Nenhum EnemySpawn foi selecionado. Verifique os pesos.");
	}
}

[System.Serializable]
public struct EnemySpawn
{
	public EnemyController Prefab;
	public int Weight;
}