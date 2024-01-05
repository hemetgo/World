using System;
using UnityEditor.XR;
using UnityEngine;

public class HandItem : MonoBehaviour
{
	[field: SerializeField] public ItemSettings ItemSettings { get; set; }

	public virtual bool IsUseEnabled => true;

	public virtual void OnEquip()
	{
		GameEvents.Player.EquipItem?.Invoke(this);
	}

	public virtual void OnUnequip()
	{

	}

	public virtual void OnUse()
	{

	}

	public bool IsTypeOf(Type type)
	{
		return type == GetType();
	}
}