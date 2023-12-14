using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Currency Drop", menuName = "HemetTools/Currency/Currency Drop")]
public class CurrencyDropSettings : ScriptableObject
{
    public CurrencySettings Currency;
    [Range(0, 1)] public float DropRate;
    public int MinAmount;
    public int MaxAmount;

    public void Claim()
    {
        if (Random.value < DropRate)
        {
            CurrencyService.ClaimDrop(this);
        }
    }

    public int GetAmount()
    {
        if (MaxAmount < MinAmount) return MinAmount;

        return Random.Range(MinAmount, MaxAmount + 1);
    }
}

