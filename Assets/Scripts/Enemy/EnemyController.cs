using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
	[field: SerializeField] public Transform TargetPoint { get; set; }

	[HideInInspector] public EnemyHealth Health;
	[HideInInspector] public ItemDropCaller ItemDropCaller;
	[HideInInspector] public CurrencyDropCaller CurrencyDropCaller;

	[HideInInspector] public Animator Animator;


	private void Awake()
	{
		Health = GetComponent<EnemyHealth>();
		ItemDropCaller = GetComponent<ItemDropCaller>();
		CurrencyDropCaller = GetComponent<CurrencyDropCaller>();

		Animator = GetComponent<Animator>();

		EnemyService.RegisterEnemy(this);
	}

	private void OnDestroy()
	{
		EnemyService.UnregisterEnemy(this);
	}
}