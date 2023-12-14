using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Event", menuName = "HemetTools/Event")]
public class ScriptableEvent : ScriptableObject
{
    public Action OnInvoke;

    public void Invoke()
    {
        OnInvoke?.Invoke();
    }
}
