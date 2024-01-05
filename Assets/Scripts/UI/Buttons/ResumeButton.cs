using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : AutoSetupButton
{
	protected override void OnClick()
	{
		PauseManager.Resume();
	}
}
