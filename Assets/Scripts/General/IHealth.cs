using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public interface IHealth 
{
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }

    public void TakeDamage(int damage);

    public void Die();
}
