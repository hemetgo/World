using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
	public static bool IsPaused { get; private set; }

	private void OnEnable()
	{
		GameEvents.Inputs.OnPause += OnPause;
	}

	private void OnDisable()
	{
		GameEvents.Inputs.OnPause -= OnPause;
	}

	public static void OnPause(InputState inputState)
	{
		if (inputState == InputState.Down)
		{
			IsPaused = !IsPaused;
			GameEvents.Game.Pause?.Invoke(IsPaused);
			Time.timeScale = IsPaused ? 0: 1;
		}
	}

	public static void Resume()
	{
		IsPaused = false;
		GameEvents.Game.Pause?.Invoke(IsPaused);
		Time.timeScale = IsPaused ? 0 : 1;
	}
}
