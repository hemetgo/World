using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    [SerializeField] float _lifeTime;

    void Start()
    {
        Destroy(gameObject, _lifeTime);
    }
}
