using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScreen : MonoBehaviour
{
    [SerializeField] Animator _gameOverScreen;
    [SerializeField] Animator _victoryScreen;

	private void Awake()
	{
		_gameOverScreen.gameObject.SetActive(false);
		_victoryScreen.gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		GameEvents.Game.GameOver += ActiveGameOverScreen;
		GameEvents.Game.Victory += ActiveVictoryScreen;
	}

	private void OnDisable()
	{
		GameEvents.Game.GameOver -= ActiveGameOverScreen;
		GameEvents.Game.Victory -= ActiveVictoryScreen;
	}

	void ActiveGameOverScreen()
	{
		_gameOverScreen.gameObject.SetActive(true);
		_gameOverScreen.SetTrigger("Show");
	}

	void ActiveVictoryScreen()
	{
		_victoryScreen.gameObject.SetActive(true);
		_victoryScreen.SetTrigger("Show");
	}
}
