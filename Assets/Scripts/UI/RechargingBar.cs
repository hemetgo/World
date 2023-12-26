using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RechargingBar : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI _rechargingText;
	[SerializeField] Slider _rechargingSlider;

	[SerializeField] string _rechargingString;
	[SerializeField] string _lowAmmoString;

	HandWeapon _weapon;

	private void Update()
	{
		if (_weapon == null) return;

		_rechargingSlider.value = _weapon.RechargingPercent;
	}

	private void OnEnable()
	{
		GameEvents.Player.OnRechargingStart += OnStart;
		GameEvents.Player.OnRechargingStop += OnStop;
		GameEvents.Player.OnRechargingComplete += OnComplete;
	}

	private void OnDisable()
	{
		GameEvents.Player.OnRechargingStart -= OnStart;
		GameEvents.Player.OnRechargingStop -= OnStop;
		GameEvents.Player.OnRechargingComplete -= OnComplete;
	}

	void OnStart(HandWeapon weapon)
	{
		_weapon = weapon;
		_rechargingSlider.gameObject.SetActive(true);
	}

	void OnStop(HandWeapon weapon)
	{
		_weapon = null;
		_rechargingSlider.gameObject.SetActive(false);
	}

	void OnComplete(HandWeapon weapon)
	{
		_weapon = null;
		_rechargingSlider.gameObject.SetActive(false);
	}
}
