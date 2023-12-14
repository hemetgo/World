using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHealth
{
	[field:SerializeField] public int Health { get; set; }


	private void Awake()
	{
		EnemyService.RegisterEnemy(this);
	}

	private void OnDestroy()
	{
		EnemyService.UnregisterEnemy(this);
	}

	public void TakeDamage(int damage)
	{
		Health -= damage;

		if (Health < 0)
		{
			Die();
		}
	}

	public void Die()
	{
		Destroy(gameObject);
	}

}