using HemetTools.Inspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
	[SerializeField] Transform _treeParent;
    [SerializeField] List<RandomTree> _treePrefabs = new List<RandomTree>();

    [SerializeField] int _treesCount;

    [SerializeField] Terrain _terrain;

	[SerializeField] int _seed;
	[SerializeField] bool _clearCurrentTrees = true;
	[SerializeField] bool _randomizeRotation = true;
	[SerializeField] public float _minDistanceBetweenTrees = 3f;

	[SerializeField] List<GameObject> _spawnedTrees = new List<GameObject>();

	private GameObject SelectTreePrefab(int randomWeight)
	{
		int cumulativeWeight = 0;

		// Loop through each tree prefab and check if the random weight falls within its range
		foreach (RandomTree treePrefab in _treePrefabs)
		{
			cumulativeWeight += treePrefab.Weight;

			if (randomWeight < cumulativeWeight)
			{
				return treePrefab.Prefab;
			}
		}

		// If no tree is selected (shouldn't happen if weights are set correctly), return the first one
		return _treePrefabs[0].Prefab;
	}

#if UNITY_EDITOR
	[Button]
#endif
	public void GenerateTrees()
	{
		if (_clearCurrentTrees)
		{
			ClearTrees();
		}

		System.Random random = new System.Random(_seed);

		if (_terrain == null)
		{
			Debug.LogError("Terrain not assigned!");
			return;
		}

		TerrainData terrainData = _terrain.terrainData;

		// Calculate the total weight of all tree prefabs
		int totalWeight = _treePrefabs.Sum(treePrefab => treePrefab.Weight);

		for (int i = 0; i < _treesCount; i++)
		{
			// Generate a random weight
			int randomWeight = random.Next(0, totalWeight);

			// Select the tree based on the random weight
			GameObject selectedTreePrefab = SelectTreePrefab(randomWeight);

			RandomPositionResult randomPositionResult = GenerateRandomTreePosition(random, terrainData);

			if (!randomPositionResult.Success) return;

			// Instantiate the selected tree prefab
			GameObject tree = Instantiate(selectedTreePrefab, randomPositionResult.Position, Quaternion.identity);

			if (_randomizeRotation)
			{
				tree.transform.Rotate(0, random.Next(0, 360), 0);
			}

			tree.transform.SetParent(_treeParent);

			// Add the position of the instantiated tree to the list
			_spawnedTrees.Add(tree);
		}
	}

	private RandomPositionResult GenerateRandomTreePosition(System.Random random, TerrainData terrainData)
	{
		int triesCount = 0;
		int maxTries = 25;

		while (true)
		{
			triesCount++;
			Vector3 randomPosition = new Vector3(
				random.Next(0, (int)terrainData.size.x),
				0f,
				random.Next(0, (int)terrainData.size.z)
			);

			randomPosition += _terrain.transform.position;

			// Adjust the Y position based on the terrain height at that position
			randomPosition.y = _terrain.SampleHeight(randomPosition);

			// Check the minimum distance between trees
			bool validPosition = CheckMinimumDistance(randomPosition);

			if (validPosition)
			{
				return new RandomPositionResult(randomPosition, true);
			}

			if (triesCount >= maxTries)
			{
				return new RandomPositionResult(Vector3.zero, false);
			}
		}
	}

	private bool CheckMinimumDistance(Vector3 position)
	{
		// Check the distance to all existing trees
		foreach (GameObject tree in _spawnedTrees)
		{
			float distance = Vector3.Distance(position, tree.transform.position);

			if (distance < _minDistanceBetweenTrees)
			{
				return false;
			}
		}

		return true;
	}

#if UNITY_EDITOR
	[Button]
#endif
	public void ClearTrees()
	{
		_spawnedTrees.ForEach(tree => DestroyImmediate(tree.gameObject));
		_spawnedTrees.Clear();
	}
}

[System.Serializable]
public struct RandomTree
{
	public GameObject Prefab;
	public int Weight;
}

public class RandomPositionResult
{
	public Vector3 Position;
	public bool Success;

	public RandomPositionResult(Vector3 position, bool success)
	{
		Position = position;
		Success = success;
	}
}