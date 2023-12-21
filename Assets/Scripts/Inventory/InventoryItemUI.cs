using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] public ItemData ItemData;

    [SerializeField] TextMeshProUGUI _itemText;
	[SerializeField] Image _itemIcon;

	ItemSettings ItemSettings => InventoryReferences.GetItemSettings(ItemData.SaveID);

	Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	public void UpdateUI(ItemData itemData)
    {
        ItemData = itemData;
		_itemText.text = $"{itemData.Amount}x {itemData.SaveID}";
		_itemIcon.sprite = ItemSettings.Icon;

		PlayAnimation();
	}

	public void PlayAnimation()
	{
		_animator.SetTrigger("Interact");
	}
}
