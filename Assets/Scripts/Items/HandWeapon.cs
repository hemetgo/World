using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWeapon : HandItem
{
    public virtual WeaponSettings Settings => ItemSettings as WeaponSettings;
}
