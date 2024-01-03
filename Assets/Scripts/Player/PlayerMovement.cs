using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MovementBehaviour
{
	Vector2 _movementInput;
	Animator _animator;

	public Vector2 Velocity => _agent.velocity;

	protected override void Awake()
	{
		base.Awake();

		_animator = GetComponent<Animator>();

		_agent.updateRotation = false;
	}

	private void Update()
	{
		if (Joystick.Input) 
			_movementInput = Joystick.Input.Direction.normalized;

		if (_movementInput.magnitude == 0)
			_movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

		MoveTo(_movementInput);

		AnimationControl();
	}

	void AnimationControl()
	{
		transform.LookAt(transform.position + new Vector3(_movementInput.x, 0, _movementInput.y)); 
		_animator.SetFloat("Movement", _agent.velocity.normalized.magnitude);
	}
}
