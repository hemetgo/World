using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuilder : MonoBehaviour
{
	[SerializeField] Animator _animator;

    BuildInteractable _buildInteraable;

	private void Update()
	{
		if (_buildInteraable == null || _buildInteraable.Builded)
		{
			return;
		}

		if (_buildInteraable.HaveEnoughIngredients())
		{
			_buildInteraable.Interact();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out BuildInteractable interactable))
		{
			_buildInteraable = interactable;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent(out BuildInteractable interactable))
		{
			_buildInteraable = null;
		}
	}
}
