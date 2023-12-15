using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IHealth
{
	public static Action<IHealth> OnPlayerHealthUpdate;

    [HideInInspector] public PlayerMovement Movement;
	[HideInInspector] public PlayerHand Hand;
	[HideInInspector] public PlayerItemCollector ItemCollector;
	[HideInInspector] public PlayerCombat Combat;
	[HideInInspector] public Animator Animator;

	public static PlayerController Instance;

    public bool IsMoving => Movement.Velocity.magnitude != 0;
	public bool IsCollecting => Animator.GetBool("Punching");
	public bool IsShooting => Animator.GetBool("Shooting");

	[field: SerializeField] public int MaxHealth { get; set; } = 100;
	[field: SerializeField] public int CurrentHealth { get; set; }

	private void Awake()
	{
		Instance = this;

		Movement = GetComponent<PlayerMovement>();
		ItemCollector = GetComponent<PlayerItemCollector>();
		Hand = GetComponent<PlayerHand>();
		Combat = GetComponent<PlayerCombat>();

		Animator = GetComponent<Animator>();

		CurrentHealth = MaxHealth;
		OnPlayerHealthUpdate?.Invoke(this);
	}

	public void LookAt(Vector3 target)
	{
		transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
	}

	public void TakeDamage(int damage)
	{
		CurrentHealth -= damage;

		OnPlayerHealthUpdate?.Invoke(this);

		if (CurrentHealth <= 0)
		{
			Die();
		}
	}

	public void Die()
	{
		Destroy(gameObject);
	}
}
