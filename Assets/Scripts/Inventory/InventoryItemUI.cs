using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] public ItemData _itemData;

    [SerializeField] TextMeshProUGUI _itemText;

	Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	public void UpdateUI(ItemData itemData)
    {
        _itemData = itemData;
		_itemText.text = $"{itemData.Amount}x {itemData.SaveID}";

		PlayAnimation();
	}

	public void PlayAnimation()
	{
		_animator.SetTrigger("Interact");
	}
}
