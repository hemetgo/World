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

	public void ApplyForce()
	{
		Vector3 forceDirection = Vector3.up;
		forceDirection += new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
		float forceIntensity = 1f; 
		Vector3 force = forceDirection * forceIntensity;
		force.y = 5;
		GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
	}

	public void Setup(ItemSettings item, int amount)
	{
		_item = item;
		_amount = amount;

		_mesh = Instantiate(_item.DropMeshPrefab, transform);
	}

	private void TryCatch(InputState inputState)
	{
		if (inputState == InputState.Down)
		{
			Catch();
		}
	}

	private void Catch()
	{
		if (FastCatchEnabled())
			InventoryService.AddItem(_item, _amount);
		else
			PlayerController.Instance.Hand.SwitchCurrentItem(_item, _amount);

		GameEvents.Inputs.OnInteract -= TryCatch;
		GameEvents.Item.OnOverlapDrop?.Invoke(false);

		Destroy(gameObject);
	}

	private bool FastCatchEnabled()
	{
		ItemData savedItem = InventoryService.GetItem(_item.SaveID);

		if (savedItem == null && InventoryService.IsFull) 
		{
			return false;
		}
		else if (!_item.Cumulative && InventoryService.IsFull)
		{
			return false;
		}

		return true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			GameEvents.Item.OnOverlapDrop?.Invoke(true);
			GameEvents.Inputs.OnInteract += TryCatch;
			return;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			GameEvents.Item.OnOverlapDrop?.Invoke(false);
			GameEvents.Inputs.OnInteract -= TryCatch;
		}
	}
}
