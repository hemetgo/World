using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRewardDrop : MonoBehaviour
{
    [SerializeField, Range(0, 1)] float _dropChance;
    [SerializeField] List<RewardDrop> _drops = new List<RewardDrop>();

    public void ClaimRandomDrop()
    {
        if (_drops.Count > 0)
        {
            if (Random.value < _dropChance)
            {
                ItemDropManager.Instance.ClaimDrop(_drops, transform.position);
            }
        }
    }
}
