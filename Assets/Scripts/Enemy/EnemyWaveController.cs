using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWaveController : MonoBehaviour
{
	[SerializeField] Terrain _terrain;
	[SerializeField] List<Transform> _spawnPoints = new List<Transform>();

	[SerializeField] EnemyWaveSettings _waveSettings;
	public static int CurrentWave { get; set; }

	public static WavePhase CurrentPhase;
	public static float PrepareWaveTimer;

	private void Start()
	{
		CurrentWave = 0;
		StartCoroutine(StartWaveTimer(0, _waveSettings.PrepareWaveTime));
	}

	private void OnEnable()
	{
		GameEvents.Enemy.OnAllEnemiesDie += FinishWave;
	}

	private void OnDisable()
	{
		GameEvents.Enemy.OnAllEnemiesDie -= FinishWave;
	}

	IEnumerator StartWaveTimer(float waveWaitTime, float prepareWaveTime)
	{
		SetPhase(WavePhase.Waiting);
		yield return new WaitForSeconds(waveWaitTime);
		SetPhase(WavePhase.Preparing);

		PrepareWaveTimer = prepareWaveTime;

		while (PrepareWaveTimer > 0)
		{
			PrepareWaveTimer -= Time.deltaTime;
			yield return null;
		}

		StartWave();
	}

	public void StartWave()
    {
		CurrentWave++;

		for (int i = 0; i < _waveSettings.EnemiesToSpawn[CurrentWave]; i++)
		{
			InstantiateRandomEnemy();
		}

		SetPhase(WavePhase.InProgress);
	}

	public void FinishWave()
	{
		if (CurrentWave >= _waveSettings.EnemiesToSpawn.Length - 1)
		{
			GameEvents.Game.Victory?.Invoke();
			return;
		}

		StartCoroutine(StartWaveTimer(_waveSettings.WaveWaitTime, _waveSettings.PrepareWaveTime));

		SetPhase(WavePhase.Completed);
	}

	EnemyController InstantiateRandomEnemy()
	{
		EnemySpawn randomSpawn = _waveSettings.GetRandomEnemySpawn();
		Transform spawnPoint = GetRandomSpawnPoint();

		Vector2 randomRange = UnityEngine.Random.insideUnitCircle * _waveSettings.SpawnRange;
		Vector3 spawnPosition = spawnPoint.position + new Vector3(randomRange.x, 0, randomRange.y);
		NavMesh.SamplePosition(spawnPosition, out NavMeshHit navHit, 5, NavMesh.AllAreas);
		spawnPosition = navHit.position;

		EnemyController enemy = Instantiate(randomSpawn.Prefab, spawnPosition, Quaternion.identity);
		Instantiate(_waveSettings.EnemySpawnVFX, enemy.transform.position, Quaternion.identity);

		float terrainHeight = _terrain.SampleHeight(enemy.transform.position);
		enemy.transform.position = new Vector3(enemy.transform.position.x, terrainHeight, enemy.transform.position.z);
		enemy.transform.SetParent(transform);

		return enemy;
	}

	Transform GetRandomSpawnPoint()
	{
		return _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)];
	}

	void SetPhase(WavePhase phase)
	{
		CurrentPhase = phase;
		GameEvents.Enemy.OnWaveChangePhase?.Invoke(phase);
	}
}

public enum WavePhase
{
	Waiting, Preparing, InProgress, Completed
}