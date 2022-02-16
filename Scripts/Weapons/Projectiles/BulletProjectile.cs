using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : Projectile
{
    private Rigidbody bulletRigidbody;
    [SerializeField] float speed = 50f;
    [SerializeField] GameObject vfxHitGreen;
    [SerializeField] GameObject vfxHitRed;


    public override void Setup(RaycastHit targetHit, float damage, Transform shootingPoint)
    {
        base.Setup(targetHit, damage, shootingPoint);
        Vector3 moveDir = (targetPosition - transform.position).normalized;
        bulletRigidbody.velocity = moveDir * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
       /* if (other.GetComponent<Zombie>() != null)
        {
            //Hit Zombie
            Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
        }*/

        Destroy(gameObject);
    }
}
