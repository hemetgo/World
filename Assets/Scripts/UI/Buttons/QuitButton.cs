using UnityEngine;


public class QuitButton : AutoSetupButton
{
	protected override void OnClick()
	{
		Application.Quit();
	}
}
