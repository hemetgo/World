using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI _currentAmmoText;
	[SerializeField] TextMeshProUGUI _reserveAmmoText;

	private void OnEnable()
	{
		GameEvents.Player.OnFire += UpdateUI;
		GameEvents.Player.OnRechargingComplete += UpdateUI;
		GameEvents.Player.OnChangeWeapon += UpdateUI;
	}

	private void OnDisable()
	{
		GameEvents.Player.OnFire -= UpdateUI;
		GameEvents.Player.OnRechargingComplete -= UpdateUI;
		GameEvents.Player.OnChangeWeapon -= UpdateUI;
	}

	void UpdateUI(HandWeapon weapon)
	{
		_currentAmmoText.text = $"{weapon.CurrentBullets}/{weapon.WeaponSettings.MagazineCapacity}";
		_reserveAmmoText.text = $"{weapon.WeaponSettings.GetReserveAmmo()}";
	}
}
