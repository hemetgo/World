using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyText : MonoBehaviour
{
    [SerializeField] CurrencySettings _currency;
    TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();

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
        if (_currency)
        {
            _text.text = CurrencyService.GetCurrency(_currency.CurrencyName).ToString();
            return;
        }

        _text.text = "Without currency reference";
    }
}
