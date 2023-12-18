using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[field: SerializeField] public Transform TargetPoint { get; set; }

	[HideInInspector] public EnemyHealth Health;


	private void Awake()
	{
		Health = GetComponent<EnemyHealth>();

		EnemyService.RegisterEnemy(this);
	}

	private void OnDestroy()
	{
		EnemyService.UnregisterEnemy(this);
	}
}