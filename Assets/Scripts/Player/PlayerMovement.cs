using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MovementBehaviour
{
	Vector2 _movementInput;
	PlayerController _controller;

	public Vector2 Velocity => _agent.velocity;

	protected override void Awake()
	{
		base.Awake();

		_controller = GetComponent<PlayerController>();

		_agent.updateRotation = false;
	}

	public void Evaluate()
	{
		_movementInput = InputHelper.MovementInput;
		//if (Joystick.Input) 
		//	_movementInput = Joystick.Input.Direction.normalized;

		if (_controller.IsShooting) _movementInput = Vector2.zero;
		MoveTo(_movementInput);

		AnimationControl();
	}

	void AnimationControl()
	{
		//if (!_controller.IsShooting)
		//	transform.LookAt(transform.position + new Vector3(_movementInput.x, 0, _movementInput.y)); 
		_controller.Animator.SetFloat("Movement", _agent.velocity.normalized.magnitude);
	}
}
