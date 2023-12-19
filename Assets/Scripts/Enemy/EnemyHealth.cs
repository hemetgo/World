using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
	EnemyController _controller;

	protected override void Awake()
	{
		base.Awake();
		_controller = GetComponent<EnemyController>();
	}

	public bool IsDead => CurrentHealth <= 0;

	public override void Die()
	{
		_controller.Animator.SetTrigger("Die");
		EnemyService.UnregisterEnemy(_controller);
		GetComponent<Collider>().enabled = false;
	}

	public void DestroyEnemy()
	{
		Destroy(gameObject);
	}
}
