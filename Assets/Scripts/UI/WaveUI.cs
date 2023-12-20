using UnityEngine;
using TMPro;
public class WaveUI : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI _waveText;
	[SerializeField] TextMeshProUGUI _remainingEnemiesText;
	[SerializeField] TextMeshProUGUI _startingText;

	[SerializeField] Animator _waveCompletedUI;

	private void Start()
	{
		_waveCompletedUI.gameObject.SetActive(false);
		_remainingEnemiesText.text = "";
	}

	private void LateUpdate()
	{
		if (EnemyWaveController.CurrentPhase == WavePhase.Preparing)
		{
			_startingText.text = $"Wave starting in {EnemyWaveController.PrepareWaveTimer.ToString("0.0")}s";
		}
	}

	private void OnEnable()
	{
		GameEvents.Enemy.OnEnemyDie += OnEnemyDie;
		GameEvents.Enemy.OnWaveChangePhase += OnWaveChangePhase;
	}

	private void OnDisable()
	{
		GameEvents.Enemy.OnEnemyDie -= OnEnemyDie;
		GameEvents.Enemy.OnWaveChangePhase -= OnWaveChangePhase;
	}

	void OnWaveChangePhase(WavePhase phase)
	{
		switch (phase)
		{
			case WavePhase.Waiting:
				_waveText.text = "";
				break;
			case WavePhase.Preparing:
				break;
			case WavePhase.InProgress:
				_startingText.text = "";
				SetInProgressUI();
				break;
			case WavePhase.Completed:
				SetCompletedWaveUI();
				break;
		}
	}

	void SetInProgressUI()
	{
		_waveText.text = $"Wave {EnemyWaveController.CurrentWave}";
		_remainingEnemiesText.text = $"{EnemyService.Enemies.Count} enemies remaining";
	}

	void SetCompletedWaveUI()
	{
		_waveText.text = "";
		_remainingEnemiesText.text = "";
		_waveCompletedUI.gameObject.SetActive(false);
		_waveCompletedUI.gameObject.SetActive(true);
	}

	void OnEnemyDie(EnemyController enemy)
	{
		SetInProgressUI();
	}

}
