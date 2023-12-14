using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Currency Debugger", menuName = "HemetTools/Currency/Debugger")]
public class CurrencyDebugger : ScriptableObject
{
	[SerializeField] CurrencySettings _currency;
	[SerializeField] int _currencyValue;

	public void AddCurrency()
	{
		CurrencyService.AddCurrency(_currency.CurrencyName, _currencyValue);
		Debug.Log(_currency + " has been updated: " + CurrencyService.GetCurrency(_currency.CurrencyName));
	}

	public void SubtractCurrency()
	{
		CurrencyService.SubtractCurrency(_currency.CurrencyName, _currencyValue);
		Debug.Log(_currency + " has been updated: " + CurrencyService.GetCurrency(_currency.CurrencyName));
	}

	public void PrintCurrencies()
	{
		foreach (string key in CurrencyService.GetCurrencies().Keys)
		{
			Debug.Log(key + ": " + CurrencyService.GetCurrency(key));
		}
	}

	public void ResetCurrency()
    {
		CurrencyService.ResetCurrency(_currency.CurrencyName);
		Debug.Log(_currency + " has been reseted");
	}

	public void ResetCurrencies()
	{
		CurrencyService.ResetCurrencies();
		Debug.Log("Currencies has been reseted.");
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(CurrencyDebugger))]
public class CurrencyDebuggerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		CurrencyDebugger currencyDebugger = (CurrencyDebugger)target;

		if (GUILayout.Button("Add"))
			currencyDebugger.AddCurrency();

		if (GUILayout.Button("Subtract"))
			currencyDebugger.SubtractCurrency();

		if (GUILayout.Button("Reset"))
			currencyDebugger.ResetCurrency();

		if (GUILayout.Button("Print All"))
			currencyDebugger.PrintCurrencies();

		if (GUILayout.Button("Reset All"))
			currencyDebugger.ResetCurrencies();
	}
}
#endif