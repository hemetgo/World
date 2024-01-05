using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropCaller : MonoBehaviour
{
    [SerializeField] DropSettings _dropSettings;
    [SerializeField] bool _airDrop;

    public void ClaimRandomDrop()
    {
        if (_dropSettings.Drops.Count > 0)
        {
            if (Random.value < _dropSettings.DropChance)
            {
                if (_airDrop) 
                    ItemDropManager.Instance.InstantiateAirDrop(_dropSettings.GetWeightedRandomRarityDrop().Elements);
                else 
                    ItemDropManager.Instance.Drop(_dropSettings.GetWeightedRandomRarityDrop().Elements, transform.position);
            }
        }
    }
}
