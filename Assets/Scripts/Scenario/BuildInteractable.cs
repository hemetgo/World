using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _jobObject;
    [SerializeField] GameObject _buildedObject;
	[SerializeField] List<Ingredient> _ingredients;

	public bool Builded => _buildedObject.activeSelf;

	private void Start()
	{
		_jobObject.SetActive(true);
		_buildedObject.SetActive(false);
	}

	public void Interact()
	{
		_jobObject.SetActive(false);
		_buildedObject.SetActive(true);
		UseIngredients();
	}

	public bool HaveEnoughIngredients()
	{
		int ingredientsThatHave = 0;

		foreach(Ingredient ingredient in  _ingredients)
		{
			if (ingredient.HaveEnough()) 
			{ 
				ingredientsThatHave++; 
			}
		}

		return ingredientsThatHave >= _ingredients.Count;
	}

	void UseIngredients()
	{
		foreach(Ingredient ingredient in _ingredients)
		{
			InventoryService.RemoveItem(ingredient.ItemSettings, ingredient.Amount);
		}
	}
}

[System.Serializable]
public class Ingredient
{
	public ItemSettings ItemSettings;
	public int Amount;

	public bool HaveEnough()
	{
		ItemData itemData = InventoryService.GetItem(ItemSettings.SaveID);
		if (itemData == null) return false;

		return itemData.Amount >= Amount;
	}
}