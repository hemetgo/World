using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementBehaviour : MonoBehaviour
{
	[SerializeField] protected float movementSpeed;

    protected NavMeshAgent _agent;

	protected virtual void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();
		_agent.speed = movementSpeed;
	}

	protected virtual void MoveTo(Vector2 direction)
	{
		_agent.velocity = movementSpeed * new Vector3(direction.x, 0, direction.y);
	}
}
