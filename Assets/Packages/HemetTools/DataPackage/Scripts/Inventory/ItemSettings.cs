using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "HemetTools/Inventory/Item")]
public class ItemSettings : SaveScriptable
{
	[field: SerializeField] public string Name { get; set; }
	[field: SerializeField] public ItemCategorySettings Category { get; set; }
}
