using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "HemetTools/Inventory/Item")]
public class ItemSettings : SaveScriptable
{
	[field: SerializeField] public string Name { get; set; }
	[field: SerializeField, TextArea] public string Description { get; set; }
	[field: SerializeField] public Sprite Icon { get; set; }
	[field: SerializeField] public ItemCategorySettings Category { get; set; }
	[field: SerializeField] public bool Cumulative { get; set; }
	[field: SerializeField] public GameObject DropMeshPrefab { get; set; }
}
