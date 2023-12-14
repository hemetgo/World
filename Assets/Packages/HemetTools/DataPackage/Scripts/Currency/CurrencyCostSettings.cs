using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Currency Cost", menuName = "HemetTools/Currency/Currency Cost")]
public class CurrencyCostSettings : ScriptableObject
{
    [field: SerializeField] public CurrencySettings Currency { get; set; }
    [field: SerializeField] public int Cost { get; set; }

    public bool HaveEnoughCurrency => CurrencyService.GetCurrency(Currency) >= Cost;

    public void SubtractCost()
    {
        CurrencyService.SubtractCurrency(Currency.CurrencyName, Cost);
    }
}
