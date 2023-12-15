using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHealth
{
	[field: SerializeField] public int MaxHealth { get; set; }
	[field: SerializeField] public int CurrentHealth { get; set; }
	[field: SerializeField] public Transform TargetPoint { get; set; }


	private void Awake()
	{
		EnemyService.RegisterEnemy(this);

		CurrentHealth = MaxHealth;
	}

	private void OnDestroy()
	{
		EnemyService.UnregisterEnemy(this);
	}

	public void TakeDamage(int damage)
	{
		CurrentHealth -= damage;

		if (CurrentHealth < 0)
		{
			Die();
		}
	}

	public void Die()
	{
		Destroy(gameObject);
	}

}