using UnityEngine;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _healthText;

	private void OnEnable()
	{
		PlayerController.OnPlayerHealthUpdate += UpdateText;
	}

	private void OnDisable()
	{
		PlayerController.OnPlayerHealthUpdate -= UpdateText;
	}

	void UpdateText(IHealth playerHealth)
	{
		_healthText.text = $"HP:{playerHealth.CurrentHealth}/{playerHealth.MaxHealth}"; 
	}
}
