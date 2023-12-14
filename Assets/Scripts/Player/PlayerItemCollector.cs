using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerItemCollector : ItemCollector
{
	[SerializeField] ItemSettings _axe;

	PlayerController _controller;
	Animator _animator;

	public override bool IsCollecting { get => _animator.GetBool("Punching"); }

	protected override void Awake()
	{
		base.Awake();

		_controller = GetComponent<PlayerController>();
		_animator = GetComponent<Animator>();

		OnCollectStart.OnInvoke += StartCollect;
	}

	protected override void Update()
	{
		if (_controller.IsMoving)
		{
			if (IsCollecting)
			{
				StopCollect();
			}

			return;
		}

		if (_itemSourcesOnRange.Count == 0) 
		{
			StopCollect();
			return;
		}

		base.Update();

		if (!IsCollectLocked && _itemSourcesOnRange.Count > 0)
		{
			StartCoroutine(Collect());
		}
	}

	void StartCollect()
	{
		_controller.Hand.ActivateItem(_axe);
		_animator.SetBool("Punching", true);
	}

	void StopCollect()
	{
		_controller.Hand.DeactivateAllItems();
		_animator.SetBool("Punching", false);
	}

}