using System.Collections.Generic;
using UnityEngine;

public class InventoryReferences : MonoBehaviour
{
	[SerializeField] List<ItemReferenceList> _itemReferences = new List<ItemReferenceList>();

    public static Dictionary<string, ItemSettings> _references = new Dictionary<string, ItemSettings>();

	protected static bool _initialized;

	public virtual void Initialize()
	{
		if (_initialized) return;	

		_references = new Dictionary<string, ItemSettings>();

		foreach (ItemReferenceList reference in _itemReferences)
		{
			reference.Items.ForEach(item => _references.Add(item.SaveID, item));
		}

		_initialized = true;
	}

	public static ItemSettings GetItemSettings(string saveID)
    {
		if (!_initialized) 
			FindObjectOfType<InventoryReferences>().Initialize();
		
		if(_references.TryGetValue(saveID, out ItemSettings itemSettings)) 
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