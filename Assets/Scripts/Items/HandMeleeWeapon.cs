using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMeleeWeapon : HandWeapon
{
	public override void OnEquip()
	{
		base.OnEquip();
	}

	public override void OnUnequip()
	{
		base.OnUnequip();
	}

	public override void Use()
	{
		foreach(EnemyController enemy in EnemyService.FindEnemiesOnRange(transform.position, Settings.Range))
		{
			enemy.Health.TakeDamage(Settings.ResultDamage);
		}		
	}
}
