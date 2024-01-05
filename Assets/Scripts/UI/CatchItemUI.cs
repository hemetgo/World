using UnityEngine;

public class CatchItemUI : MonoBehaviour
{
	[SerializeField] GameObject _contextUI;

	private void OnEnable()
	{
		GameEvents.Item.OnOverlapDrop += OnDropOverlap;
	}

	private void OnDisable()
	{
		GameEvents.Item.OnOverlapDrop -= OnDropOverlap;
	}

	void OnDropOverlap(bool overlaps)
	{
		_contextUI.gameObject.SetActive(overlaps);
	}
}
