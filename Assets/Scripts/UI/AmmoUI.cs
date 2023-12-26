using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI _ammoText;

	private void OnEnable()
	{
		GameEvents.Player.OnRechargingComplete += UpdateUI;
		GameEvents.Player.OnChangeWeapon += UpdateUI;
		GameEvents.Player.OnAmmoUpdate += UpdateUI;
	}

	private void OnDisable()
	{
		GameEvents.Player.OnRechargingComplete -= UpdateUI;
		GameEvents.Player.OnChangeWeapon -= UpdateUI;
		GameEvents.Player.OnAmmoUpdate -= UpdateUI;
	}

	void UpdateUI(HandWeapon weapon)
	{
		_ammoText.text = $"{weapon.CurrentBullets}/{weapon.WeaponSettings.GetReserveAmmo()}";
	}
}
