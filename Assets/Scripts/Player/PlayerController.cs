using UnityEngine;

public class PlayerController : MonoBehaviour, IHealth
{
    [HideInInspector] public PlayerMovement Movement;
	[HideInInspector] public PlayerHand Hand;
	[HideInInspector] public PlayerItemCollector ItemCollector;
	[HideInInspector] public PlayerCombat Combat;

	public static PlayerController Instance;

    public bool IsMoving => Movement.Velocity.magnitude != 0;

	[field: SerializeField] public int Health { get; set; } = 100;

	private void Awake()
	{
		Instance = this;

		Movement = GetComponent<PlayerMovement>();
		ItemCollector = GetComponent<PlayerItemCollector>();
		Hand = GetComponent<PlayerHand>();
		Combat = GetComponent<PlayerCombat>();
	}

	public void LookAt(Vector3 target)
	{
		transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
	}

	public void TakeDamage(int damage)
	{
		Health -= damage;

		if (Health <= 0)
		{
			Die();
		}
	}

	public void Die()
	{
		Destroy(gameObject);
	}
}
