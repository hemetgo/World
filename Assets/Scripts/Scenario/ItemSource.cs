using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSource : MonoBehaviour, IInteractable
{
    [SerializeField] ItemSettings _item;
    [SerializeField] int _totalAmount;
	[SerializeField] public CollectingToolSettings RequiredTool;

	Animator _animator;

	protected void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	public void Interact()
	{
		if (_totalAmount <= 0) return;

		InventoryService.AddItem(_item, 1);
		_totalAmount -= 1;

		_animator.SetTrigger("Interact");

		if(_totalAmount <= 0 )
		{
			BreakIt();
		}
	}

	protected virtual void BreakIt()
	{
		Destroy(gameObject, .15f);
	}
}
