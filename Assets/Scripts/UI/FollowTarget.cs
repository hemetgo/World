using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] Transform _target;
    Vector3 _offset;

	private void Awake()
	{
		_offset = transform.position - _target.position;
		transform.SetParent(null);
	}

	private void Update()
	{
		transform.position = _target.position + _offset;
	}
}
