using HemetTools.Inspector;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
	[SerializeField] Transform _treeParent;
    [SerializeField] List<GameObject> _treePrefabs = new List<GameObject>();

    [SerializeField] int _treesCount;

    [SerializeField] Terrain _terrain;

    [Button]
    public void GenerateTrees()
    {
		if (_terrain == null)
		{
			Debug.LogError("Terrain not assigned!");
			return;
		}

		TerrainData terrainData = _terrain.terrainData;

		for (int i = 0; i < _treesCount; i++)
		{
			Vector3 randomPosition = new Vector3(
				Random.Range(0f, terrainData.size.x),
				0f,
				Random.Range(0f, terrainData.size.z)
			);

			randomPosition += _terrain.transform.position;

			// Adjust the Y position based on the terrain height at that position
			randomPosition.y = _terrain.SampleHeight(randomPosition);

			// Instantiate a random tree prefab
			GameObject randomTreePrefab = _treePrefabs[Random.Range(0, _treePrefabs.Count)];
			GameObject tree = Instantiate(randomTreePrefab, randomPosition, Quaternion.identity);

			tree.transform.SetParent(_treeParent);
		}
	}
}
