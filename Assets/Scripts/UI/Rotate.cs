using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] Vector3 _rotateSpeed;

	private void Update()
	{
		transform.Rotate(_rotateSpeed * Time.deltaTime);
	}
}
