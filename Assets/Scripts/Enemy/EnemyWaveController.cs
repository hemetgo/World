using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{
	[SerializeField] Terrain _terrain;
	[SerializeField] List<Transform> _spawnPoints = new List<Transform>();
	[SerializeField] float _spawnRange;

    [SerializeField] List<EnemySpawn> _enemiesPrefabs = new List<EnemySpawn>();

	[SerializeField] int _currentWave;
	[SerializeField] int _enemiesToSpawn;

	private void Start()
	{
		StartWave();
	}

	public void StartWave()
    {
		for (int i = 0; i < _enemiesToSpawn; i++)
		{
			InstantiateRandomEnemy();
		}
    }

	Enemy InstantiateRandomEnemy()
	{
		// Obtém um spawn aleatório
		EnemySpawn randomSpawn = GetRandomEnemySpawn();

		// Obtém um ponto de spawn aleatório
		Transform spawnPoint = GetRandomSpawnPoint();

		// Instancia o inimigo no ponto de spawn
		Enemy enemy = Instantiate(randomSpawn.Prefab, spawnPoint.position, Quaternion.identity);

		// Adiciona uma variação aleatória à posição
		Vector2 randomRange = UnityEngine.Random.insideUnitCircle * _spawnRange;
		enemy.transform.Translate(randomRange.x, 0, randomRange.y);

		// Define a altura do inimigo para a altura do terreno no ponto de spawn
		float terrainHeight = _terrain.SampleHeight(enemy.transform.position);
		enemy.transform.position = new Vector3(enemy.transform.position.x, terrainHeight, enemy.transform.position.z);

		// Define o pai do inimigo para o objeto transform
		enemy.transform.SetParent(transform);

		return enemy;
	}

	EnemySpawn GetRandomEnemySpawn()
	{
		int totalWeight = 0;

		// Calcula o peso total
		foreach (var spawn in _enemiesPrefabs)
		{
			totalWeight += spawn.Weight;
		}

		// Seleciona um número aleatório dentro do intervalo total de pesos
		int randomValue = UnityEngine.Random.Range(0, totalWeight);

		// Itera sobre a lista até encontrar o spawn correspondente ao número aleatório
		foreach (var spawn in _enemiesPrefabs)
		{
			if (randomValue < spawn.Weight)
			{
				return spawn;
			}

			randomValue -= spawn.Weight;
		}

		// Caso não seja encontrado nenhum spawn (isso não deve acontecer se os pesos estiverem configurados corretamente)
		throw new Exception("Nenhum spawn foi selecionado. Verifique os pesos.");
	}

	Transform GetRandomSpawnPoint()
	{
		return _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)];
	}
}

[System.Serializable]
public struct EnemySpawn
{
    public Enemy Prefab;
    public int Weight;
}
