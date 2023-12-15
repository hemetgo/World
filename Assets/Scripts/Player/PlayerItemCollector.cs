using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerItemCollector : ItemCollector
{
	[SerializeField] CollectingToolSettings _collectingToolSettings;

	PlayerController _controller;
	Animator _animator;

	public override bool IsCollectEnabled
	{
		get
		{
			if (_controller.IsMoving || _controller.IsShooting) return false;
			return base.IsCollectEnabled;
		}
	}

	protected override void Awake()
	{
		base.Awake();

		_controller = GetComponent<PlayerController>();
		_animator = GetComponent<Animator>();
	}

	protected override void Update()
	{
		base.Update();

		bool isCollectEnabled = IsCollectEnabled;
		_animator.SetBool("Punching", isCollectEnabled);

		if (isCollectEnabled)
		{
			_controller.Hand.ActivateItem(_collectingToolSettings);
			_animator.speed = _collectingToolSettings.Efficiency;
		}
		else
		{
			_controller.Hand.DeactivateAllItems();
			_animator.speed = 1;
		}
	}
}