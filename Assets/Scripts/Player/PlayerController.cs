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
		if (!IsCollecting && !IsShooting) 
		{
			Animator.speed = 1;
		}
	}

	public void LookAt(Vector3 target)
	{
		transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
	}

	public Vector3 MouseInput
    {
        get
        {
			// Crie um raio a partir da posição do mouse na tela
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			// Crie um plano no qual você deseja projetar o ponto do mouse
			Plane plane = new Plane(Vector3.up, Vector3.zero);

			float hitDistance;

			// Intersecte o raio com o plano
			if (plane.Raycast(ray, out hitDistance))
			{
				// Obtenha a posição do ponto de interseção no espaço do mundo
				Vector3 worldMousePosition = ray.GetPoint(hitDistance);
				worldMousePosition.y = transform.position.y + 1;
				return worldMousePosition;
			}

			// Se o botão do mouse não estiver sendo pressionado, retorne Vector3.zero
			return Vector3.zero;
		}
    }
}
