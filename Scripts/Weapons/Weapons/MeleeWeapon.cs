using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeapon : Weapon
{
    protected bool inAttack = false;
    protected bool inCombo = false;
    protected bool inParry = false;
    public override void OnLeftClickDown()
    {
        Attack();
    }

    public override void OnLeftClickUp()
    {

    }

    public override void OnRightClickDown()
    {
        Parry();
        inParry = true;
    }

    public override void OnRightClickUp()
    {
        inParry = true;
    }

    public override void OnEquip(WeaponManager manager)
    {
        base.OnEquip(manager);
    }

    protected void Update()
    {
        if (inParry)
        {
            //Block attack
        }
    }

    protected abstract void Attack();
    protected abstract void Parry();
}
