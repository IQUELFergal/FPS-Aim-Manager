using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Gun : RangeWeapon
{
    [Tooltip("Delay between two shots")]
    [Min(0.01f)] [SerializeField] protected float shotDelay;
    [Tooltip("Number of projectiles per magazine")]
    [Min(1)] [SerializeField] private int magazineSize;
    [Tooltip("Number of projectiles fired per shot")]
    [Min(1)] [SerializeField] protected int projectileCount;
    [Tooltip("Dispersion of the projectiles in degrees")]
    [Range(0, 90)] [SerializeField] protected float dispersion;
    [Tooltip("Dispersion of the projectiles in degrees")]
    [Range(0, 1)] [SerializeField] protected float aimDispersionModifier;

    [Header("Projectile")]
    [SerializeField] protected VisualEffect muzzleFlash;
    [Tooltip("Damage inflicted by each projectile")]
    [SerializeField] protected float projectileDamage;
    [Tooltip("Prefab of the projectile used by the weapon")]
    [SerializeField] protected Projectile projectile;
    [Tooltip("Layer on which projectiles will collide")]
    [SerializeField] private LayerMask projectileColliderMask;

    private float currentDispersion;

    public int MagazineSize { get => magazineSize;}

    private void Start()
    {
        currentDispersion = dispersion;
        projectileInMagazine = magazineSize;
    }

    protected override void Aim()
    {
        manager.SetAimCamera();
        currentDispersion = dispersion * (1 - aimDispersionModifier);
    }
    protected override void Unaim()
    {
        manager.SetNormalCamera();
        currentDispersion = dispersion;
    }

    protected override void Reload()
    {
        projectileInMagazine = magazineSize;
        //manager
    }

    protected override void Shoot()
    {
        if (Time.time - lastShotTime > shotDelay && projectileInMagazine > 0 )
        {
            for (int i = 0; i < projectileCount; i++)
            {
                //Get target point
                Vector3 targetPoint = (dispersion > 0 ? RandomizeTargetPoint(shootingPoint.position, manager.AimTargetManager.MouseWorldPosition) : manager.AimTargetManager.MouseWorldPosition);
                Vector3 aimDir = (targetPoint - shootingPoint.position).normalized;
                //Shoot a raycast in the randomized direction
                RaycastHit targetHit;
                Ray ray = new Ray(shootingPoint.position, aimDir);
                Physics.Raycast(ray, out targetHit, 999f, projectileColliderMask);
                //Instantiate and setup to the correct target point
                Projectile bullet = manager.ProjectileObjectPool.Get();
                bullet.Setup(targetHit, projectileDamage, shootingPoint);
            }
            lastShotTime = Time.time;
            projectileInMagazine--;
            if (muzzleFlash != null)
            {
                muzzleFlash.Play();
            }
            manager.ProjectileObjectPool.count--;
            Debug.Log("gerer la suppression de munition ici et au rechargement");
        }
    }
    Vector3 RandomizeTargetPoint(Vector3 shootingPoint, Vector3 targetPoint)
    {
        float angle = Random.Range(0f, 1f) * 2 * Mathf.PI;
        float maxRadius = Mathf.Tan(dispersion * Mathf.Deg2Rad) * (targetPoint - shootingPoint).z;
        float radius = maxRadius * Random.Range(0f, 1f);

        // Cartesian coordinates
        float x = radius * Mathf.Cos(angle);
        float y = radius * Mathf.Sin(angle);
        return targetPoint + new Vector3(x, y, 0f);
    }
}
