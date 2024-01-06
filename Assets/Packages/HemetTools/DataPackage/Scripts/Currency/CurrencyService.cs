
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEditor;

public class CurrencyService
{
    private static CurrencyData currencyData;

    public static Action OnAnyCurrencyUpdated;

    public static void ClaimDrop(CurrencyDropSettings drop)
    {
        EnsureCurrencyData();
		AddCurrency(drop.Currency.SaveID, drop.GetAmount());
    }

	public static int GetCurrency(string currencyId)
	{
		EnsureCurrencyData();
		return currencyData.GetCurrency(currencyId);
	}

	public static int GetCurrency(CurrencySettings currency)
    {
        EnsureCurrencyData();
        return GetCurrency(currency.SaveID);
    }

    public static Dictionary<string, int> GetCurrencies()
    {
        EnsureCurrencyData();
        return currencyData.Currencies;
    }

    public static void AddCurrency(CurrencySettings currency, int amount)
    {
        EnsureCurrencyData();
		AddCurrency(currency.SaveID, amount);
    }

    public static void AddCurrency(string currencyId, int amount)
    {
        EnsureCurrencyData();

        currencyData.AddValue(currencyId, amount);
        OnAnyCurrencyUpdated?.Invoke();

        Save();
    }

	public static void SubtractCurrency(CurrencySettings currency, int amount)
	{
        EnsureCurrencyData();
		SubtractCurrency(currency.SaveID, amount);
	}

	public static void SubtractCurrency(string currencyId, int amount)
    {
        EnsureCurrencyData();

        currencyData.SubtractValue(currencyId, amount);
        OnAnyCurrencyUpdated?.Invoke();

        Save();
    }

	public static void SetCurrency(CurrencySettings currency, int amount)
	{
        EnsureCurrencyData();
		SetCurrency(currency.SaveID, amount);
	}

	public static void SetCurrency(string currencyId, int amount)
    {
        EnsureCurrencyData();

        currencyData.SetCurrency(currencyId, amount);
        OnAnyCurrencyUpdated?.Invoke();

        Save();
    }

    private static void EnsureCurrencyData()
    {
        if (currencyData == null)
        {
            currencyData = DataManager.Load<CurrencyData>();
        }
    }

    private static void Save()
    {
        EnsureCurrencyData();
        DataManager.Save(currencyData);
	}

    public static void ResetCurrency(string currencyId)
    {
        EnsureCurrencyData();

		currencyData.SetCurrency(currencyId, 0);
        Save();

        OnAnyCurrencyUpdated?.Invoke();
    }

#if UNITY_EDITOR
    [MenuItem("HemetTools/Currency/Reset currencies")]
#endif
    public static void ResetCurrencies()
	{
        EnsureCurrencyData();

        currencyData.Clear();
		Save();

		OnAnyCurrencyUpdated?.Invoke();
    }
}

[System.Serializable]
public class CurrencyData
{
    [JsonProperty] public Dictionary<string, int> Currencies = new Dictionary<string, int>();

    public CurrencyData() { }

    public void Clear()
    {
        Currencies.Clear();
    }

    public int GetCurrency(string currencyId)
    {
        if (Currencies.TryGetValue(currencyId, out int value))
        {
            return value;
        }

        Currencies.Add(currencyId, 0);
        return Currencies[currencyId];
    }

    public void SetCurrency(string currencyId, int value)
    {
        Currencies[currencyId] = value;
    }

    public void AddValue(string currencyId, int value)
    {
        if (!Currencies.ContainsKey(currencyId))
        {
            Currencies[currencyId] = 0;
        }

        Currencies[currencyId] += value;
    }

    public void SubtractValue(string currencyId, int value)
    {
        AddValue(currencyId, -value);
    }
}