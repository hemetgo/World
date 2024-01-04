using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponGUI : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI _feedbackText;
	[SerializeField] Slider _rechargingSlider;

	HandFireWeapon _weapon;

	private void Update()
	{
		if (_weapon == null) return;

		_rechargingSlider.value = _weapon.RechargingPercent;
	}

	private void OnEnable()
	{
		GameEvents.Player.OnRechargingStart += OnRechargingStart;
		GameEvents.Player.OnEquipItem += OnRechargingStop;
		GameEvents.Player.OnRechargingStop += OnRechargingStop;
		GameEvents.Player.OnRechargingComplete += OnRechargingComplete;
	}

	private void OnDisable()
	{
		GameEvents.Player.OnRechargingStart -= OnRechargingStart;
		GameEvents.Player.OnEquipItem -= OnRechargingStop;
		GameEvents.Player.OnRechargingStop -= OnRechargingStop;
		GameEvents.Player.OnRechargingComplete -= OnRechargingComplete;
	}

	void OnRechargingStart(HandFireWeapon weapon)
	{
		_weapon = weapon;
		_feedbackText.gameObject.SetActive(true);
		_rechargingSlider.gameObject.SetActive(true);
	}

	void OnRechargingStop(HandItem item)
	{
		_weapon = null;
		_feedbackText.gameObject.SetActive(false);
		_rechargingSlider.gameObject.SetActive(false);
	}

	void OnRechargingComplete(HandFireWeapon weapon)
	{
		_weapon = null;
		_feedbackText.gameObject.SetActive(false);
		_rechargingSlider.gameObject.SetActive(false);
	}
}
