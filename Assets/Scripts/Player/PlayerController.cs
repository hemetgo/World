using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public PlayerMovement Movement;
	[HideInInspector] public PlayerHand Hand;
	[HideInInspector] public PlayerItemCollector ItemCollector;
	[HideInInspector] public PlayerCombat Combat;
	[HideInInspector] public PlayerHealth Health;
	[HideInInspector] public Animator Animator;

	public static PlayerController Instance;

    public bool IsMoving => Movement.Velocity.magnitude != 0;
	public bool IsCollecting => Animator.GetBool("Punching");
	public bool IsShooting => Animator.GetBool("Shooting");

	private void Awake()
	{
		Instance = this;

		Movement = GetComponent<PlayerMovement>();
		ItemCollector = GetComponent<PlayerItemCollector>();
		Hand = GetComponent<PlayerHand>();
		Combat = GetComponent<PlayerCombat>();
		Health = GetComponent<PlayerHealth>();

		Animator = GetComponent<Animator>();
	}

	private void Update()
	{
		if (!IsMoving && !IsShooting) 
		{
			Animator.speed = 1;
		}
	}

	public void LookAt(Vector3 target)
	{
		transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
	}
}
