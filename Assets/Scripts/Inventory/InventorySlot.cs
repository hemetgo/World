using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
	[SerializeField] WeaponItemType _weaponItemType;
	[SerializeField] GameObject _selectBorder;

	[SerializeField] TextMeshProUGUI _itemText;
	[SerializeField] Image _itemIcon;
	[SerializeField] Image _background;

	[SerializeField] Animator _animator;

	public ItemData ItemData => WeaponInventoryService.GetItem(_weaponItemType);

	public ItemSettings ItemSettings => InventoryReferences.GetItemSettings(ItemData.SaveID);

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		GameEvents.Player.EquipItem += OnItemEquipped;
		WeaponInventoryService.OnInventoryChanged += UpdateUI;
	}

	private void OnDisable()
	{
		GameEvents.Player.EquipItem -= OnItemEquipped;
		WeaponInventoryService.OnInventoryChanged -= UpdateUI;
	}

	void OnItemEquipped(HandItem item)
	{
		if (PlayerHand.CurrentItemType == _weaponItemType)
		{
			Select(true);
			PlayAnimation();
			return;
		}

		Select(false);
	}

	public void UpdateUI()
	{
		if (ItemData == null)
		{
			_itemText.gameObject.SetActive(false);
			_itemIcon.gameObject.SetActive(false);
			return;
		}

		_itemText.gameObject.SetActive(true);
		_itemIcon.gameObject.SetActive(true);
		_itemText.text = ItemData.Amount > 1 ? $"{ItemData.Amount}" : "";
		_itemIcon.sprite = ItemSettings.Icon;

		PlayAnimation();
	}

	public void Select(bool select)
	{
		if (!_selectBorder) return;
		_selectBorder.SetActive(select);
	}

	public void PlayAnimation()
	{
		_animator.SetTrigger("Interact");
	}
}
