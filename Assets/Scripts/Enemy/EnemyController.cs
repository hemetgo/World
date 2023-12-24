using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[field: SerializeField] public Transform TargetPoint { get; set; }

	[HideInInspector] public EnemyHealth Health;
	[HideInInspector] public EnemyRewardDrop Drop;

	[HideInInspector] public Animator Animator;


	private void Awake()
	{
		Health = GetComponent<EnemyHealth>();
		Drop = GetComponent<EnemyRewardDrop>();

		Animator = GetComponent<Animator>();

		EnemyService.RegisterEnemy(this);
	}

	private void OnDestroy()
	{
		EnemyService.UnregisterEnemy(this);
	}
}