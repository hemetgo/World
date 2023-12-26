using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponGUI : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI _feedbackText;
	[SerializeField] Slider _rechargingSlider;

	HandWeapon _weapon;

	private void Update()
	{
		if (_weapon == null) return;

		_rechargingSlider.value = _weapon.RechargingPercent;
	}

	private void OnEnable()
	{
		GameEvents.Player.OnRechargingStart += OnRechargingStart;
		GameEvents.Player.OnChangeWeapon += OnRechargingStop;
		GameEvents.Player.OnRechargingStop += OnRechargingStop;
		GameEvents.Player.OnRechargingComplete += OnRechargingComplete;
	}

	private void OnDisable()
	{
		GameEvents.Player.OnRechargingStart -= OnRechargingStart;
		GameEvents.Player.OnChangeWeapon -= OnRechargingStop;
		GameEvents.Player.OnRechargingStop -= OnRechargingStop;
		GameEvents.Player.OnRechargingComplete -= OnRechargingComplete;
	}

	void OnRechargingStart(HandWeapon weapon)
	{
		_weapon = weapon;
		_feedbackText.gameObject.SetActive(true);
		_rechargingSlider.gameObject.SetActive(true);
	}

	void OnRechargingStop(HandWeapon weapon)
	{
		_weapon = null;
		_feedbackText.gameObject.SetActive(false);
		_rechargingSlider.gameObject.SetActive(false);
	}

	void OnRechargingComplete(HandWeapon weapon)
	{
		_weapon = null;
		_feedbackText.gameObject.SetActive(false);
		_rechargingSlider.gameObject.SetActive(false);
	}
}
