using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	[SerializeField] HandWeapon _currentWeapon;

	public bool IsAttackEnabled { get;set; }

	PlayerController _controller;
	Animator _animator;

	private void Start()
	{
		_controller = GetComponent<PlayerController>();
		_animator = GetComponent<Animator>();

		IsAttackEnabled = true;
	}

	private void Update()
	{
		AttackController();
	}

	private void AttackController()
	{
		if (!IsAttackEnabled) 
			return;

		if (EnemyService.HaveEnemies)
		{
			Enemy targetEnemy = EnemyService.FindClosestEnemy(transform.position);

			if (Vector3.Distance(transform.position, targetEnemy.transform.position) > _currentWeapon.WeaponSettings.Range)
				return;

			StartCoroutine(Attack(targetEnemy));
		}
	}

	IEnumerator Attack(Enemy targetEnemy)
	{
		IsAttackEnabled = false;

		if (!_controller.IsMoving)
		{
			_controller.Hand.ActivateItem(_currentWeapon.WeaponSettings);
			_controller.LookAt(targetEnemy.transform.position);

			_animator.SetTrigger("Fire");
		}

		yield return new WaitForSeconds(_currentWeapon.WeaponSettings.Cooldown);
		_controller.Hand.DeactivateAllItems();
		IsAttackEnabled = true;
	}

	public void Fire()
	{
		Enemy targetEnemy = EnemyService.FindClosestEnemy(transform.position);
		_currentWeapon.Fire(targetEnemy);
	}
}
