using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	[field: SerializeField] public ItemCategorySettings WeaponCategory { get; set; }

	public HandWeapon CurrentWeapon
	{
		get
		{
			if (_controller.Hand.IsHolding(WeaponCategory) == false) return null;
			else return _controller.Hand.CurrentHandItem as HandWeapon;
		}
	}
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
		if (CurrentWeapon == null) 
			return false;

		if (!CurrentWeapon.HaveBullets) return false;

		if (Input.GetButton("Fire1"))
        {
			_controller.LookAt(_controller.MouseInput);
			_animator.speed = CurrentWeapon.WeaponSettings.FireRate;
			return true;
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
