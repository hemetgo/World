using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryItemUI : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] public ItemData ItemData;

    [SerializeField] TextMeshProUGUI _itemText;
	[SerializeField] Image _itemIcon;

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
		_itemText.text = $"{itemData.Amount}x {ItemSettings.Name}";
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

	public void OnPointerUp(PointerEventData eventData)
	{
		_isPressing = false;
		
		if (_downTimer <= _holdTime)
		{
			SelectItem();
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_isPressing = true;
		_downTimer = 0;
	}
}
