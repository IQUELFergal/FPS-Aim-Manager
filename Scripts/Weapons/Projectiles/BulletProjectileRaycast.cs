using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectileRaycast : Projectile
{
    [SerializeField] GameObject vfxHit;

    public override void Setup(RaycastHit targetHit, float damage, Transform shootingPoint)
    {
        base.Setup(targetHit, damage, shootingPoint);
        GetComponent<TrailRenderer>().enabled = true;
    }

    private void Update()
    {
        if (targetCollider != null)
        {
            float distanceBefore = Vector3.Distance(transform.position, targetPosition);

            Vector3 moveDir = (targetPosition - transform.position).normalized;
            float moveSpeed = 200f;
            transform.position += moveDir * moveSpeed * Time.deltaTime;

            float distanceAfter = Vector3.Distance(transform.position, targetPosition);

            if (distanceBefore < distanceAfter)
            {
                HealthStat health;
                if (targetCollider.TryGetComponent(out health))
                {
                    health.TakeDamage(damage);
                }

                GameObject hitGameObject = Instantiate(vfxHit, targetPosition, Quaternion.identity);
                GetComponent<TrailRenderer>().enabled = false;
                //Don't notify if the projectile directly hit the zombie
                if (targetCollider.GetComponentInChildren<AIHearing>() == null)
                {
                    AIHearing.Notify(hitGameObject, 5);
                }
                FindObjectOfType<ProjectileObjectPool>().Return(this);
            }
        }
    }
}
