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
		if (!IsCollecting && !IsShooting) 
		{
			Animator.speed = 1;
		}

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
