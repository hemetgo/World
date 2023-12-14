using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleZombie : MovementBehaviour
{
    [SerializeField] float _attackRange;
    [SerializeField] int _attackDamage;
    [SerializeField] float _attackCooldown;

    bool IsAttackEnabled { get; set; }

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

		if (IsAttackEnabled) {
			if (Vector3.Distance(transform.position, Player.transform.position) <= _attackRange)
			{
				StartCoroutine(AttackPlayer());
			}		
		}
	}

	public IEnumerator AttackPlayer()
    {
		IsAttackEnabled = false;

		Player.TakeDamage(_attackDamage);
		_animator.SetTrigger("Attack");

		yield return new WaitForSeconds(_attackCooldown);

		IsAttackEnabled = true;
	}
}
