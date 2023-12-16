using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCombat : MonoBehaviour
{
	[SerializeField] HandWeapon _currentWeapon;

	PlayerController _controller;
	Animator _animator;

	private void Start()
	{
		_controller = GetComponent<PlayerController>();
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		bool IsAttacking = AttackController();
		_animator.SetBool("Shooting", IsAttacking);
	}

	private bool AttackController()
	{
		if (_controller.IsMoving || !EnemyService.HaveEnemies) return false;

		Enemy targetEnemy = EnemyService.FindClosestEnemy(transform.position); 
			
		if (targetEnemy)
		{
			float enemyDistance = Vector3.Distance(transform.position, targetEnemy.transform.position);
			if (enemyDistance <= _currentWeapon.WeaponSettings.Range)
			{
				_controller.Hand.ActivateItem(_currentWeapon.WeaponSettings);
				_controller.LookAt(targetEnemy.transform.position);
				_animator.speed = _currentWeapon.WeaponSettings.FireRate;
				return true;
			}
		}

		return false;
	}

	/// <summary>
	/// Is called on shooting animation
	/// </summary>
	public void Fire()
	{
		Enemy targetEnemy = EnemyService.FindClosestEnemy(transform.position);
		_currentWeapon.Fire(targetEnemy);
	}
}
