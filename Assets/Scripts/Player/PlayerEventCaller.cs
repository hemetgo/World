using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerEventCaller : MonoBehaviour
{
	[SerializeField] ScriptableEvent OnCollect;

	public void Invoke()
	{
		OnCollect.Invoke();
	}
}
