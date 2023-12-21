using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryReferences : MonoBehaviour
{
	[SerializeField] List<ItemReferenceList> _itemReferences = new List<ItemReferenceList>();

    public static Dictionary<string, ItemSettings> _references = new Dictionary<string, ItemSettings>();

	public void Initialize()
	{
		_references = new Dictionary<string, ItemSettings>();

		foreach (ItemReferenceList reference in _itemReferences)
		{
			reference.Items.ForEach(item => _references.Add(item.SaveID, item));
		}
	}

	public static ItemSettings GetItemSettings(string saveID)
    {
		if (_references.TryGetValue(saveID, out ItemSettings itemSettings)) 
			return itemSettings;

		Debug.LogError($"Fail when trying to get the {saveID} reference. Not found!");
		return null;
    }
}

[System.Serializable]
public struct ItemReferenceList
{
	public string ListName;
	public List<ItemSettings> Items;
}