using UnityEngine;
using TMPro;

public class PlayerBulletsUI : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI _bulletsText;

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
		_bulletsText.text = $"{weapon.CurrentBullets}/{weapon.WeaponSettings.MagazineCapacity}"; 
	}
}
