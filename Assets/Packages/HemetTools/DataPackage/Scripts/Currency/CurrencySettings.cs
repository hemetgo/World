using UnityEngine;

[CreateAssetMenu(fileName = "New Currency", menuName = "HemetTools/Currency/Currency")]
public class CurrencySettings : SaveScriptable
{
    [field: SerializeField] public string CurrencyName { get; set; }
    [field: SerializeField] public Sprite CurrencyIcon { get; set; }
}