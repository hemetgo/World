using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Survival/Item/Melee Weapon")]
public class WeaponSettings : ItemSettings
{
	[field: SerializeField] public int Damage { get; set; }
	[field: SerializeField] public float Range { get; set; }
	[field: SerializeField, Range(0, 1)] public float CriticalRate { get; set; }
	[field: SerializeField, Range(1, 5)] public float CriticalDamage { get; set; }
	[field: SerializeField, Range(.8f, 10)] public float AttackSpeed { get; set; }

	public ResultDamage ResultDamage
	{
		get
		{
			if (Random.value < CriticalRate)
			{
				return new ResultDamage(true, (int)(Damage * CriticalDamage));
			}

			return new ResultDamage(false, Damage);
		}
	}
}

public class ResultDamage
{
	public bool IsCritical;
	public int Value;

	public ResultDamage(bool isCritical, int value)
	{
		IsCritical = isCritical;
		Value = value;
	}
}