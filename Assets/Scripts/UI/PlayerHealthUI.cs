using UnityEngine;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _healthText;

	private void OnEnable()
	{
		GameEvents.Player.OnHealthChanged += UpdateUI;
	}

	private void OnDisable()
	{
		GameEvents.Player.OnHealthChanged -= UpdateUI;
	}

	void UpdateUI(Health playerHealth)
	{
		_healthText.text = $"HP:{playerHealth.CurrentHealth}/{playerHealth.MaxHealth}"; 
	}
}
