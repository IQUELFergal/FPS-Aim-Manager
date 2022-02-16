using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private AnimationClip weaponAnimation;
    protected WeaponManager manager = null;

    public AnimationClip WeaponAnimation { get => weaponAnimation;}

    public abstract void OnLeftClickDown();
    public abstract void OnLeftClickUp();
    public abstract void OnRightClickDown();
    public abstract void OnRightClickUp();
    public abstract void OnReload();

    public virtual void OnEquip(WeaponManager manager)
    {
        this.manager = manager;
    }
}


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

public abstract class RangeWeapon : Weapon
{
    [Tooltip("Transform of the shooting point")]
    [SerializeField] protected Transform shootingPoint;
    [Tooltip("Sets the weapon to be automatic or semi automatic")]
    [SerializeField] protected bool automatic;

    protected bool isShooting;
    protected float lastShotTime = 0;
    protected float currentShotDelay = 0;
    [SerializeField] protected int projectileInMagazine = 0;

    public override void OnLeftClickDown()
    {
        Shoot();
        isShooting = automatic;
    }

    public override void OnLeftClickUp()
    {
        isShooting = false;
    }

    public override void OnRightClickDown()
    {
        Aim();
    }

    public override void OnRightClickUp()
    {
        Unaim();
    }

    public override void OnReload()
    {
        Reload();
    }

    public override void OnEquip(WeaponManager manager)
    {
        base.OnEquip(manager);
    }

    protected void Update()
    {
        if (isShooting)
        {
            Shoot();
        }
    }

    protected abstract void Shoot();
    protected abstract void Aim();
    protected abstract void Unaim();
    protected abstract void Reload();

    
}