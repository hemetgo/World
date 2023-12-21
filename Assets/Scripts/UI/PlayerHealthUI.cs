using UnityEngine;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _healthText;
	[SerializeField] GameObject _damageVignette;

	private void OnEnable()
	{
		GameEvents.Player.OnHealthChanged += UpdateUI;
		GameEvents.Player.OnTakeDamage += TriggerDamageVignette;
	}

	private void OnDisable()
	{
		GameEvents.Player.OnHealthChanged -= UpdateUI;
		GameEvents.Player.OnTakeDamage -= TriggerDamageVignette;
	}

	void UpdateUI(Health playerHealth)
	{
		_healthText.text = $"HP:{playerHealth.CurrentHealth}/{playerHealth.MaxHealth}"; 
	}

	void TriggerDamageVignette()
	{
		_damageVignette.SetActive(false);
		_damageVignette.SetActive(true);
	}
}
