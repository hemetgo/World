using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	[field: SerializeField] public ItemCategorySettings WeaponCategory { get; set; }

	private bool _isFiring;

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

	private void OnEnable()
	{
		GameEvents.Inputs.OnFire += SetFiring;
	}

	private void OnDisable()
	{
		GameEvents.Inputs.OnFire -= SetFiring;
	}

	private void Start()
	{
		_controller = GetComponent<PlayerController>();
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		AttackController();
	}

	void SetFiring(bool firing)
	{
		_isFiring = firing;
		_animator.SetBool("Firing", firing);
	}

	private void AttackController()
	{
		if (CurrentWeapon == null || !CurrentWeapon.HaveBullets)
		{
			SetFiring(false);
			return;
		}

		if (_isFiring)
		{
			_controller.LookAt(InputHelper.GetRelativeMouseWorldPosition(transform));
			_animator.speed = CurrentWeapon.WeaponSettings.FireRate;
		}
	}

}
