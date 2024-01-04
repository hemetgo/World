using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI _ammoText;

	private void OnEnable()
	{
		GameEvents.Player.OnRechargingComplete += UpdateUI;
		GameEvents.Player.OnEquipItem += UpdateUI;
		GameEvents.Player.OnAmmoUpdate += UpdateUI;
	}

	private void OnDisable()
	{
		GameEvents.Player.OnRechargingComplete -= UpdateUI;
		GameEvents.Player.OnEquipItem -= UpdateUI;
		GameEvents.Player.OnAmmoUpdate -= UpdateUI;
	}

	void UpdateUI(HandItem item)
	{
		if (item.IsTypeOf(typeof(HandFireWeapon)))
		{
			HandFireWeapon weapon = (HandFireWeapon)item;
			_ammoText.text = $"{weapon.CurrentBullets}/{weapon.FireWeaponSettings.GetReserveAmmo()}";
		}
		else
		{
			_ammoText.text = "";
		}
	}
}
