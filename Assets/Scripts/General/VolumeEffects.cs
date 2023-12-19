using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeEffects : MonoBehaviour
{
	Volume _volume;

	private void Awake()
	{
		_volume = GetComponent<Volume>();
	}

	private void OnEnable()
	{
		GameEvents.Player.OnTakeDamage += OnTakeDamage;
	}

	private void OnDisable()
	{
		GameEvents.Player.OnTakeDamage -= OnTakeDamage;
	}

	void OnTakeDamage()
	{
		StartCoroutine(BlinkVignete());
	}

	IEnumerator BlinkVignete()
	{
		if (_volume.profile.TryGet(out Vignette vignette))
		{
			Color color = vignette.color.value;
			vignette.color.value = Color.red;
			yield return new WaitForSeconds(.15f);
			vignette.color.value = color;
		}
	}
}
