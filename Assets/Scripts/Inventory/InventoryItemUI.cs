using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] public ItemData ItemData;

    [SerializeField] TextMeshProUGUI _itemText;
	[SerializeField] Image _itemIcon;

	Button _button;

	ItemSettings ItemSettings => InventoryReferences.GetItemSettings(ItemData.SaveID);
	PlayerController Player => PlayerController.Instance;

	Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_button = GetComponent<Button>();
	}

	private void OnEnable()
	{
		_button.onClick.AddListener(SelectItem);
	}

	private void OnDisable()
	{
		_button.onClick.RemoveListener(SelectItem);
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

	public void SelectItem()
	{
		Player.Hand.SelectItem(ItemSettings);
	}
}
