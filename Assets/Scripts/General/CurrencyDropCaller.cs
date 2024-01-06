using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HemetTools.RandomTools;

public class CurrencyDropCaller : MonoBehaviour
{
    [SerializeField] CurrencySettings _currency;
    [SerializeField] RandomIntValue _dropAmount;

    public void ClaimRandomCurrency()
    {
        CurrencyService.AddCurrency(_currency, _dropAmount.Value);
	}
}
