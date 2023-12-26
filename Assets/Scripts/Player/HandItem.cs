using UnityEngine;

public class HandItem : MonoBehaviour
{
	[field: SerializeField] public ItemSettings ItemSettings { get; set; }

	public virtual void OnActivated()
	{

	}

	public virtual void OnDeactivated()
	{

	}
}