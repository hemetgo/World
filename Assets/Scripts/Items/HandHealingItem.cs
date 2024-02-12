using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHealingItem : HandItem
{
	[SerializeField] int _healValue;

	public override void Use()
	{
		base.Use();
		PlayerController.Instance.Health.Heal(_healValue);
	}
}
