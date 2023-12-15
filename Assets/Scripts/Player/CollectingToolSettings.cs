using UnityEngine;

[CreateAssetMenu(fileName = "New Collecting Tool", menuName = "Survival/Item/Collecting Tool")]
public class CollectingToolSettings : ItemSettings
{
    [SerializeField, Range(.8f, 10)] public float Efficiency;

}
