using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	[field: SerializeField] public List<ItemCategorySettings> WeaponCategories { get; set; }

	private bool _isFiring;

	private HandWeapon CurrentWeapon
	{
		get
		{
			if (!IsHoldingWeapon) 
				return null;

			return _controller.Hand.CurrentHandItem as HandWeapon;
		}
	}

	bool IsHoldingWeapon => _controller.Hand.IsHolding(WeaponCategories);

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

	public void Evaluate()
	{
		AttackController();
	}

	void SetFiring(InputState inputState)
	{
		if (!IsHoldingWeapon || !CurrentWeapon.IsUseEnabled || inputState == InputState.Up)
		{
			_isFiring = false;
			_animator.SetBool("Firing", false);
			_animator.SetBool("Punching", false);
			return;
		}

		_isFiring = true;

		
		bool meleeWeapon = CurrentWeapon.GetType() == typeof(HandMeleeWeapon);
		_animator.SetBool("Firing", !meleeWeapon);
		_animator.SetBool("Punching", meleeWeapon);
	}

	private void AttackController()
	{
		if (!IsHoldingWeapon || !CurrentWeapon.IsUseEnabled)
		{
			SetFiring(InputState.Up);
			return;
		}

		if (_isFiring)
		{
			_animator.speed = CurrentWeapon.Settings.AttackSpeed;
		}
	}
}
