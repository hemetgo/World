using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static GameEvents;

public class EnemyHealth : Health
{
	public GameObject BloodVFX;

	EnemyController _controller;

	protected override void Awake()
	{
		base.Awake();
		_controller = GetComponent<EnemyController>();
	}

	public override void TakeDamage(ResultDamage damage)
	{
		Instantiate(BloodVFX, _controller.TargetPoint.position, Quaternion.identity);
		base.TakeDamage(damage);
	}

	public override void Die()
	{
		_controller.Animator.SetTrigger("Die");

		_controller.Drop.ClaimRandomDrop();

		EnemyService.UnregisterEnemy(_controller);
		GetComponent<Collider>().enabled = false;
		GetComponent<NavMeshAgent>().enabled = false;
	}

	public void DestroyEnemy()
	{
		Destroy(gameObject);
	}
}
