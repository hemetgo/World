using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneButton : AutoSetupButton
{
    [SerializeField, Scene] string _scene;

	protected override void OnClick()
	{
		SceneManager.LoadScene(_scene);
	}
}
