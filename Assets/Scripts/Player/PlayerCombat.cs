using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	[field: SerializeField] public ItemCategorySettings WeaponCategory { get; set; }

	HandWeapon CurrentWeapon => _controller.Hand.CurrentHandItem as HandWeapon;

	PlayerController _controller;
	Animator _animator;

	private void Start()
	{
		_controller = GetComponent<PlayerController>();
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		bool isAttacking = AttackController();
		_animator.SetBool("Shooting", isAttacking);
	}

	private bool AttackController()
	{
		if (_controller.Hand.IsHolding(WeaponCategory) == false) 
			return false;

		if (_controller.IsMoving || !EnemyService.HaveEnemies || !CurrentWeapon.HaveBullets) return false;

		EnemyController targetEnemy = EnemyService.FindClosestEnemy(transform.position); 
			
		if (targetEnemy)
		{
			float enemyDistance = Vector3.Distance(transform.position, targetEnemy.transform.position);
			if (enemyDistance <= CurrentWeapon.WeaponSettings.Range)
			{
				_controller.LookAt(targetEnemy.transform.position);
				_animator.speed = CurrentWeapon.WeaponSettings.FireRate;
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
		CurrentWeapon.Fire(targetEnemy);
	}
}
