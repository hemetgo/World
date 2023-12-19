using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[field: SerializeField] public Transform TargetPoint { get; set; }

	[HideInInspector] public EnemyHealth Health;
	[HideInInspector] public Animator Animator;


	private void Awake()
	{
		Health = GetComponent<EnemyHealth>();
		Animator = GetComponent<Animator>();

		EnemyService.RegisterEnemy(this);
	}
}