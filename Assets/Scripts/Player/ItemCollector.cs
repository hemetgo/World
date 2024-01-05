using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] protected float _collectCooldown;
    [SerializeField] protected float _delayToStart;
    [SerializeField] protected float _delayToEnd;

	[SerializeField] protected List<ItemSource> _itemSourcesOnRange = new List<ItemSource>();

	public virtual bool IsCollecting { get; set; }

	public virtual bool IsCollectEnabled
	{
		get
		{
			_itemSourcesOnRange.RemoveAll(source => source == null);
			return _itemSourcesOnRange.Count > 0;
		}
	}

	protected virtual void Awake()
	{
	}

	public virtual void Evaluate()
	{
	}

	public virtual void CollectItems()
	{
		_itemSourcesOnRange.RemoveAll(source => source == null);

		if (_itemSourcesOnRange.Count > 0)
		{
			_itemSourcesOnRange.ForEach(source => source.Interact());
		}
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out ItemSource source))
		{
			_itemSourcesOnRange.Add(source);
		}
	}

	protected virtual void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent(out ItemSource source))
		{
			_itemSourcesOnRange.Remove(source);
		}
	}
}
