using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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