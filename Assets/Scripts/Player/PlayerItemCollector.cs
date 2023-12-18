using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerItemCollector : ItemCollector
{
	[SerializeField] CollectingToolSettings _axeSettings;

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
			_animator.speed = _axeSettings.Efficiency;
		}
	}

	public ItemSettings GetRequiredCollectingTool()
	{
		if (_itemSourcesOnRange.Count > 0) return _itemSourcesOnRange[0].RequiredTool;
		else return null;
	}
}