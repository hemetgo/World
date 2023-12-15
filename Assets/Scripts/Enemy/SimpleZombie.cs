using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleZombie : MovementBehaviour
{
    [SerializeField] float _attackRange;
    [SerializeField] int _attackDamage;
    [SerializeField, Range(.5f, 5f)] float _attackSpeed;

    bool IsAttackEnabled { get; set; }

	public bool IsAttacking => _animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking");

	PlayerController Player => PlayerController.Instance;
	Animator _animator;

	protected override void Awake()
	{
		base.Awake();

		IsAttackEnabled = true;

		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		_agent.SetDestination(Player.transform.position);
		_animator.SetFloat("Velocity", _agent.velocity.normalized.magnitude);
		AttackController();
	}

	public void AttackController()
    {
		_agent.isStopped = IsAttacking;
		
		if (IsAttackEnabled)
		{
			if (Vector3.Distance(transform.position, Player.transform.position) <= _attackRange)
			{
				_animator.SetBool("Attacking", true);
				_animator.speed = _attackSpeed;
				return;
			}
		}

		_animator.SetBool("Attacking", false);
		_animator.speed = 1;

	}

	public void CauseDamageToPlayer()
	{
		if (Vector3.Distance(transform.position, Player.transform.position) <= _attackRange * 1.25f)
		{
			Player.TakeDamage(_attackDamage);
		}
	}
}
