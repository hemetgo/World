using UnityEngine;
using TMPro;
public class WaveUI : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI _waveText;
	[SerializeField] TextMeshProUGUI _remainingEnemiesText;

	private void OnEnable()
	{
		GameEvents.Enemy.OnEnemyDie += UpdateUI;
		GameEvents.Enemy.OnWaveStart += UpdateUI;
	}

	private void OnDisable()
	{
		GameEvents.Enemy.OnEnemyDie -= UpdateUI;
		GameEvents.Enemy.OnWaveStart -= UpdateUI;

	}

	void UpdateUI()
	{
		_waveText.text = $"Wave {EnemyService.CurrentWave}";
		_remainingEnemiesText.text = $"{EnemyService.Enemies.Count} enemies remaining";
	}

	void UpdateUI(EnemyController enemy)
	{
		UpdateUI();
	}
}
