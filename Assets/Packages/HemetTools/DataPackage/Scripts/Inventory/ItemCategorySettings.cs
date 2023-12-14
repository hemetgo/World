using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Category", menuName = "HemetTools/Inventory/Item Category")]
public class ItemCategorySettings : ScriptableObject
{
	[field: SerializeField] public string Name { get; set; }

}
