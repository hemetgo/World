
using System;
using UnityEngine;
using HemetTools.Inspector;
using static GameEvents;

public class Health : MonoBehaviour
{
    [field: SerializeField] public int MaxHealth { get; set; }
	[field:SerializeField, ReadOnly] public int CurrentHealth { get; set; }
    [SerializeField] DamageFeedback _damageFeedbackPrefab;

	public bool IsDead => CurrentHealth <= 0;

	protected virtual void Awake()
	{
        Initialize();
	}

	public virtual void Initialize()
    {
        CurrentHealth = MaxHealth;
    }

	public virtual void TakeDamage(ResultDamage damage) 
    {
        CurrentHealth -= damage.Value;

		SpawnDamageFeedback(damage);

		if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    void SpawnDamageFeedback(ResultDamage damage)
    {
        DamageFeedback feedback = Instantiate(_damageFeedbackPrefab, transform.position + Vector3.up / 2, Quaternion.identity);
        feedback.Setup(damage);
    }

    public virtual void Die() 
    {
        Destroy(gameObject);
    }
}
