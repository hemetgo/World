using UnityEngine;

public class ItemDrop : MonoBehaviour
{
	[SerializeField] ItemSettings _item;
	[SerializeField] int _amount;

	[SerializeField] GameObject _mesh;
	[SerializeField] AnimationCurve _curve;
	[SerializeField] float _upSpeed;
	[SerializeField] float _rotateSpeed;

	float _timer;

	private void Update()
	{
		_timer += Time.deltaTime * _upSpeed / 10;
		if (_timer > 1)	_timer = 0;

		_mesh.transform.position = transform.position + Vector3.up * _curve.Evaluate(_timer);
		_mesh.transform.Rotate(0, _rotateSpeed * 10 * Time.deltaTime, 0);
	}

	private void Catch()
	{
		InventoryService.AddItem(_item, _amount);
		GameEvents.Item.OnOverlapDrop(false);
		Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			GameEvents.Item.OnOverlapDrop(true);
			GameEvents.Item.OnTryCatchItem += Catch;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			GameEvents.Item.OnOverlapDrop(false);
			GameEvents.Item.OnTryCatchItem -= Catch;
		}
	}
}
