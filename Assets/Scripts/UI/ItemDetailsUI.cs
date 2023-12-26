using UnityEngine;
using TMPro;

public class ItemDetailsUI : MonoBehaviour
{
    [SerializeField] GameObject _canvas;
	[SerializeField] TextMeshProUGUI _nameText;
	[SerializeField] TextMeshProUGUI _amountText;
	[SerializeField] TextMeshProUGUI _descriptionText;

	private void OnEnable()
	{
		GameEvents.UI.OnOpenItemDetails += ShowItem;
	}

	private void OnDisable()
	{
		GameEvents.UI.OnOpenItemDetails -= ShowItem;
	}

	void ShowItem(ItemSettings settings, ItemData item)
	{
		_canvas.SetActive(true);

		_nameText.text = settings.Name;
		_amountText.text = $"x{item.Amount}";
		_descriptionText.text = $"{settings.Description}";
	}

	public void Hide()
	{
		_canvas.SetActive(false);
	}
}
