
using System;
using UnityEngine;
using HemetTools.Inspector;

public class Health : MonoBehaviour
{
    [field: SerializeField] public int MaxHealth { get; set; }
	[field:SerializeField, ReadOnly] public int CurrentHealth { get; set; }

	public bool IsDead => CurrentHealth <= 0;

	protected virtual void Awake()
	{
        Initialize();
	}

	public virtual void Initialize()
    {
        CurrentHealth = MaxHealth;
    }

	public virtual void TakeDamage(int damage) 
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die() 
    {
        Destroy(gameObject);
    }
}
