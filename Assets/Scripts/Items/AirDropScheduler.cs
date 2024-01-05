using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDropScheduler : ItemDropCaller
{
    [SerializeField] Vector2 _dropInterval;

	private void Start()
	{
		StartCoroutine(CallDrop());
	}

	IEnumerator CallDrop()
    {
        yield return new WaitForSeconds(Random.Range(_dropInterval.x, _dropInterval.y));
        ClaimRandomDrop();
		StartCoroutine(CallDrop());
	}
}
