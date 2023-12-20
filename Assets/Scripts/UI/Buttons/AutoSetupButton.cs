using UnityEngine;
using UnityEngine.UI;

public class AutoSetupButton : MonoBehaviour
{
	protected virtual void OnEnable()
	{
		GetComponent<Button>().onClick.AddListener(OnClick);
	}

	protected virtual void OnDisable()
	{
		GetComponent<Button>().onClick.RemoveListener(OnClick);
	}

	protected virtual void OnClick()
	{

	}
}
