using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Vector3 targetPosition;
    protected Collider targetCollider;
    protected float damage;
    public virtual void Setup(RaycastHit targetHit, float damage, Transform shootingPoint)
    {
        targetPosition = targetHit.point;
        targetCollider = targetHit.collider;
        this.damage = damage;
        transform.position = shootingPoint.position;
        transform.rotation = shootingPoint.rotation;
    }
}
