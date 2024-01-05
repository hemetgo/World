using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField] GameObject _pausePanel;

	private void OnEnable()
	{
		GameEvents.Game.Pause += OnPause;
	}

	private void OnDisable()
	{
		GameEvents.Game.Pause -= OnPause;
	}

	void OnPause(bool pause)
	{
		_pausePanel.SetActive(pause);
	}
}
