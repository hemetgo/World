using UnityEngine;

public class ItemCamera : MonoBehaviour
{
    [SerializeField] GameObject _itemObject;
    [SerializeField] Transform _itemPoint;

	private void OnEnable()
	{
		GameEvents.UI.OnOpenItemDetails += Setup;
	}

	private void OnDisable()
	{
		GameEvents.UI.OnOpenItemDetails -= Setup;
	}

	public void Setup(ItemSettings item, ItemData itemData)
    {
        if (_itemObject != null)
            Destroy(_itemObject);

		if (item.DropMeshPrefab == null)
			return;

        _itemObject = Instantiate(item.DropMeshPrefab, _itemPoint);
    }
}
