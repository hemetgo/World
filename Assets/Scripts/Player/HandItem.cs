using UnityEngine;

public class HandItem : MonoBehaviour
{
	[field: SerializeField] public ItemSettings ItemSettings { get; set; }

	public virtual void OnEquip()
	{

	}

	public virtual void OnUnequip()
	{

	}

	public virtual void OnUse()
	{

	}
}