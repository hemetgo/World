using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRewardDrop : MonoBehaviour
{
    [SerializeField] DropSettings _dropSettings;

    public void ClaimRandomDrop()
    {
        if (_dropSettings.Drops.Count > 0)
        {
            if (Random.value < _dropSettings.DropChance)
            {
                ItemDropManager.Instance.ClaimDrop(_dropSettings.Drops, transform.position);
            }
        }
    }
}
