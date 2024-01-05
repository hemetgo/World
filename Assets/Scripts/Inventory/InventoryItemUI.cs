using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryItemUI : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] public ItemData ItemData;

	[SerializeField] GameObject _selectBorder;

    [SerializeField] TextMeshProUGUI _itemText;
	[SerializeField] Image _itemIcon;
	[SerializeField] Image _background;

	ItemSettings ItemSettings => InventoryReferences.GetItemSettings(ItemData.SaveID);
	PlayerController Player => PlayerController.Instance;

	float _holdTime = .5f;
	bool _isPressing;
	float _downTimer;

	Animator _animator;


	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		if (_isPressing)
		{
			_downTimer += Time.deltaTime;
			if (_downTimer > _holdTime)
			{
				GameEvents.UI.OnOpenItemDetails?.Invoke(ItemSettings, ItemData);
				_isPressing = false;
			}
		}
	}

	public void UpdateUI(ItemData itemData)
    {
        ItemData = itemData;
		_itemText.text = itemData.Amount > 1 ? $"{itemData.Amount}" : "";
		_itemIcon.sprite = ItemSettings.Icon;

		PlayAnimation();
	}

	public void Select(bool select)
	{
		_selectBorder.SetActive(select);
	}

	public void PlayAnimation()
	{
		_animator.SetTrigger("Interact");
	}

	public void SelectItem()
	{
		Player.Hand.SelectItem(ItemSettings);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_isPressing = false;
		
		if (_downTimer <= _holdTime)
		{
			SelectItem();
		}

		_background.color = Color.white;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_isPressing = true;
		_downTimer = 0;
		_background.color = Color.green;
	}
}
