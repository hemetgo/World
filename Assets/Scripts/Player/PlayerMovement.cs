using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MovementBehaviour
{
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
		MoveTo(Joystick.Input.Direction.normalized);

		AnimationControl();
	}

	void AnimationControl()
	{
		transform.LookAt(transform.position + new Vector3(Joystick.Input.Direction.normalized.x, 0, Joystick.Input.Direction.normalized.y)); 
		_animator.SetFloat("Movement", _agent.velocity.normalized.magnitude);
	}
}
