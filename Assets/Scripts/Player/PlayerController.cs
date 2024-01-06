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
	public bool IsCollecting => Animator.GetBool("Collecting");
	public bool IsShooting => Animator.GetBool("Firing") || Animator.GetBool("Punching");

	public void Initialize()
	{
		Instance = this;

		Movement = GetComponent<PlayerMovement>();
		ItemCollector = GetComponent<PlayerItemCollector>();
		Hand = GetComponent<PlayerHand>();
		Combat = GetComponent<PlayerCombat>();
		Health = GetComponent<PlayerHealth>();

		Hand.Initialize();
		Combat.Initialize();

		Animator = GetComponent<Animator>();
	}

	private void Update()
	{
		if ((!IsCollecting && !IsShooting) || Health.IsDead) 
		{
			Animator.speed = 1;
		}

		if (!Health.IsDead && !PauseManager.IsPaused)
		{
			Movement.Evaluate();
			ItemCollector.Evaluate();
			Hand.Evaluate();
			Combat.Evaluate();
			Health.Evaluate();
			LookToCursor();
		}
	}

	public void LookToCursor()
	{
		LookAt(InputHelper.GetRelativeMouseWorldPosition(transform));
	}

	public void LookAt(Vector3 target)
	{
		transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
	}

	public Vector3 WorldMousePosition
    {
        get
        {
			Vector3 mouseScreenPosition = Input.mousePosition;
			Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
			return worldMousePosition;
		}
    }
}
