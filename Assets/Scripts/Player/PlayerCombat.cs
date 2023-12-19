using System.ComponentModel;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	[field: SerializeField] public WeaponSettings CurrentWeaponSettings { get; set; }
	[HemetTools.Inspector.ReadOnly] HandWeapon _currentHandWeapon;

	PlayerController _controller;
	Animator _animator;

	private void Start()
	{
		_controller = GetComponent<PlayerController>();
		_animator = GetComponent<Animator>();

		SetCurrentWeapon(CurrentWeaponSettings);
	}

	private void Update()
	{
		bool isAttacking = AttackController();
		_animator.SetBool("Shooting", isAttacking);
	}

	public void SetCurrentWeapon(WeaponSettings settings) 
	{
		CurrentWeaponSettings = settings;
		_currentHandWeapon = _controller.Hand.GetWeapon(settings);
	}

	private bool AttackController()
	{
		if (_controller.IsMoving || !EnemyService.HaveEnemies) return false;

		EnemyController targetEnemy = EnemyService.FindClosestEnemy(transform.position); 
			
		if (targetEnemy)
		{
			float enemyDistance = Vector3.Distance(transform.position, targetEnemy.transform.position);
			if (enemyDistance <= CurrentWeaponSettings.Range)
			{
				_controller.LookAt(targetEnemy.transform.position);
				_animator.speed = CurrentWeaponSettings.FireRate;
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
		EnemyController targetEnemy = EnemyService.FindClosestEnemy(transform.position);
		_currentHandWeapon.Fire(targetEnemy);
	}
}
