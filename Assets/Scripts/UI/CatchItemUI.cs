using UnityEngine;
using UnityEngine.UI;

public class CatchItemUI : MonoBehaviour
{
	[SerializeField] Button _catchButton;

	private void OnEnable()
	{
		GameEvents.Item.OnOverlapDrop += OnDropOverlap;

		_catchButton.onClick.AddListener(() => GameEvents.Item.OnTryCatchItem());
	}

	private void OnDisable()
	{
		GameEvents.Item.OnOverlapDrop -= OnDropOverlap;

		_catchButton.onClick.RemoveListener(() => GameEvents.Item.OnTryCatchItem());
	}

	void OnDropOverlap(bool overlaps)
	{
		_catchButton.gameObject.SetActive(overlaps);
	}
}
