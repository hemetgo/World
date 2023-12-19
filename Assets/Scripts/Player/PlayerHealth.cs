using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
	[SerializeField] GameObject _bloodVFX;

	public override void TakeDamage(int damage)
	{
		base.TakeDamage(damage);

		GameEvents.Player.OnHealthChanged.Invoke(this);
		Instantiate(_bloodVFX, transform.position + Vector3.up, Quaternion.identity);
		GameEvents.Player.OnTakeDamage?.Invoke();
	}
}   
