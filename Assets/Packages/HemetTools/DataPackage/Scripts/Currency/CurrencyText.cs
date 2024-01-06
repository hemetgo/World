using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyText : MonoBehaviour
{
    [SerializeField] CurrencySettings _currency;
    [SerializeField] string _prefix;

    private void Start()
    {
         OnCurrencyUpdated();
    }

    private void OnEnable()
    {
        CurrencyService.OnAnyCurrencyUpdated += OnCurrencyUpdated;
    }

    private void OnDisable()
    {
        CurrencyService.OnAnyCurrencyUpdated -= OnCurrencyUpdated;
    }

    private void OnCurrencyUpdated()
    {
        if (_currency != null)
        {
			GetComponent<TextMeshProUGUI>().text = $"{_prefix}{CurrencyService.GetCurrency(_currency)}";
            return;
        }

		GetComponent<TextMeshProUGUI>().text = "No currency reference";
    }
}
