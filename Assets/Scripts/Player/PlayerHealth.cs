using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
	[SerializeField] GameObject _bloodVFX;

	PlayerController _controller;

	protected override void Awake()
	{
		base.Awake();

		_controller = GetComponent<PlayerController>();
	}

	private void Start()
	{
		GameEvents.Player.OnHealthChanged?.Invoke(this);
	}

	public override void TakeDamage(ResultDamage damage)
	{
		base.TakeDamage(damage);

		GameEvents.Player.OnHealthChanged.Invoke(this);
		Instantiate(_bloodVFX, transform.position + Vector3.up, Quaternion.identity);

		GameEvents.Player.OnTakeDamage?.Invoke();
	}

	public override void Die()
	{
		_controller.Animator.SetTrigger("Die");

		_controller.Movement.enabled = false;
		_controller.Combat.enabled = false;
		_controller.ItemCollector.enabled = false;

		GameEvents.Game.GameOver?.Invoke();
	}
}   
