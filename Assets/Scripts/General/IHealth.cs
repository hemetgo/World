using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public interface IHealth 
{
    public int Health { get; set; }

    public void TakeDamage(int damage);

    public void Die();
}
