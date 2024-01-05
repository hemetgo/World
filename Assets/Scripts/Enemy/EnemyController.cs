using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
	[field: SerializeField] public Transform TargetPoint { get; set; }

	[HideInInspector] public EnemyHealth Health;
	[HideInInspector] public ItemDropCaller Drop;

	[HideInInspector] public Animator Animator;


	private void Awake()
	{
		Health = GetComponent<EnemyHealth>();
		Drop = GetComponent<ItemDropCaller>();

		Animator = GetComponent<Animator>();

		EnemyService.RegisterEnemy(this);
	}

	private void OnDestroy()
	{
		EnemyService.UnregisterEnemy(this);
	}
}