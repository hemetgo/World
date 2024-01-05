using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponGUI : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI _feedbackText;
	[SerializeField] TextMeshProUGUI _currentAmmoText;
	[SerializeField] Slider _rechargingSlider;

	HandFireWeapon _weapon;

	private void Update()
	{
		if (_weapon == null) return;

		_rechargingSlider.value = _weapon.RechargingPercent;
	}

	void OnAmmoUpdate(HandItem item)
	{
		UpdateFeedbackText(item, FeedbackTextState.CurrentAmmo);
	}

	void UpdateFeedbackText(HandItem item, FeedbackTextState state)
	{
		switch (state)
		{
			case FeedbackTextState.Reloading:
				ActiveText(_feedbackText);
				_feedbackText.text = "RELOADING";
				break;

			case FeedbackTextState.CurrentAmmo:
				ActiveText(_currentAmmoText);
				if (item.IsTypeOf(typeof(HandFireWeapon)))
				{
					HandFireWeapon weapon = (HandFireWeapon)item;
					_currentAmmoText.text = $"{weapon.CurrentBullets}/{weapon.FireWeaponSettings.GetReserveAmmo()}";
				}
				else
				{
					_currentAmmoText.text = "";
				}
				break;

			case FeedbackTextState.None:
				ActiveText(null);

				break;
		}
	}

	private void OnEnable()
	{
		GameEvents.Player.OnAmmoUpdate += OnAmmoUpdate;
		GameEvents.Player.EquipItem += OnEquipItem;
		GameEvents.Player.StartReloading += OnStartReloading;
		GameEvents.Player.StopReloading += OnStopReloading;
		GameEvents.Player.CompleteReloading += OnCompleteReloading;
	}

	private void OnDisable()
	{
		GameEvents.Player.OnAmmoUpdate -= OnAmmoUpdate;
		GameEvents.Player.EquipItem -= OnEquipItem;
		GameEvents.Player.StartReloading -= OnStartReloading;
		GameEvents.Player.StopReloading -= OnStopReloading;
		GameEvents.Player.CompleteReloading -= OnCompleteReloading;
	}

	void ActiveText(TextMeshProUGUI text)
	{
		List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>()
		{
			_feedbackText, _currentAmmoText
		};

		texts.ForEach(t => t.gameObject.SetActive(t == text));
	}

	void OnEquipItem(HandItem handItem)
	{
		if (handItem.IsTypeOf(typeof(HandFireWeapon)))
		{
			_weapon = handItem as HandFireWeapon;
			UpdateFeedbackText(handItem, FeedbackTextState.CurrentAmmo);
		} 
		else 
			UpdateFeedbackText(handItem, FeedbackTextState.None);

		_rechargingSlider.gameObject.SetActive(false);
	}

	void OnStartReloading(HandFireWeapon weapon)
	{
		_weapon = weapon;
		UpdateFeedbackText(weapon, FeedbackTextState.Reloading);
		_rechargingSlider.gameObject.SetActive(true);
	}

	void OnStopReloading(HandItem handItem)
	{
		_weapon = null;
		UpdateFeedbackText(handItem, FeedbackTextState.None);
		_rechargingSlider.gameObject.SetActive(false);
	}

	void OnCompleteReloading(HandFireWeapon weapon)
	{
		_weapon = null;
		UpdateFeedbackText(weapon, FeedbackTextState.CurrentAmmo);
		_rechargingSlider.gameObject.SetActive(false);
	}

	enum FeedbackTextState
	{
		Reloading, NoAmmo, CurrentAmmo, None
	}
}