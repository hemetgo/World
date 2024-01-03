using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using static GameEvents;

public class SimpleZombie : MovementBehaviour
{
	[SerializeField] float _detectionRange;
	[SerializeField] float _attackRange;
	[SerializeField] int _attackDamage;
	[SerializeField, Range(.5f, 5f)] float _attackSpeed;

	bool _isAggressive;
	bool IsAggressive
	{
		get
		{
			if (Player == null)
				return false;

			if (_isAggressive == true) 
				return true;

			_isAggressive = Vector3.Distance(transform.position, Player.transform.position) <= _detectionRange;
			return _isAggressive;
		}
	}
	bool IsAttackEnabled { get; set; }

	public bool IsAttacking => _controller.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking");

	PlayerController Player => PlayerController.Instance;
	EnemyController _controller;

	protected override void Awake()
	{
		base.Awake();

		_controller = GetComponent<EnemyController>();

		IsAttackEnabled = true;

		_controller.Animator = GetComponent<Animator>();
		_agent.updateRotation = false;
	}

	private void Update()
	{
		if (_agent == false) 
			return;

		if (_controller.Health.IsDead && _agent.isOnNavMesh)
		{
			_agent.isStopped = true;
			return;
		}

		if (!IsAggressive) return;

		if (Player == null)
		{
			_agent.isStopped = true; 
			return;
		}

		if (_agent.isOnNavMesh) 
			_agent.SetDestination(Player.transform.position);
		transform.LookAt(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z));

		_controller.Animator.SetFloat("Velocity", _agent.velocity.normalized.magnitude);
		AttackController();
	}

	public void AttackController()
    {
		if (Player.Health.IsDead) 
			return;

		if (_agent.isOnNavMesh)
			_agent.isStopped = IsAttacking;
		
		if (IsAttackEnabled)
		{
			if (Vector3.Distance(transform.position, Player.transform.position) <= _attackRange)
			{
				_controller.Animator.SetBool("Attacking", true);
				_controller.Animator.speed = _attackSpeed;
				return;
			}
		}

		_controller.Animator.SetBool("Attacking", false);
		_controller.Animator.speed = 1;

	}

	public void CauseDamageToPlayer()
	{
		if (Player == null || Player.Health.IsDead) 
			return;

		if (Vector3.Distance(transform.position, Player.transform.position) <= _attackRange * 1.25f)
		{
			Player.Health.TakeDamage(_attackDamage);
		}
	}
}
