using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MeleeWeapon
{
    [SerializeField] new Light light;

    protected override void Parry()
    {
        light.enabled = !light.enabled;
    }

    protected override void Attack()
    {
        
    }

    public override void OnReload()
    {
        
    }
}
