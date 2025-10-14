using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public int bulletDamage;
    private void OnCollisionEnter(Collision objectWeHit)
    {
        if (objectWeHit.gameObject.CompareTag("Targets"))
        {
            // If the bullet hits a target, apply damage
            
            //Hitting the Zombie
            objectWeHit.gameObject.GetComponent<BaseZombie>().TakeDamage(bulletDamage);

            CreateBulletImpactEffect(objectWeHit);

            Destroy(gameObject);
        }

        if (objectWeHit.gameObject.CompareTag("Wall"))
        {
            // Destroy the bullet on any collision except with the player
            //print("Hit Wall!");

            CreateBulletImpactEffect(objectWeHit);

            Destroy(gameObject);
        }
    }
    
    void CreateBulletImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];
        GameObject hole = Instantiate(
            GlobalReferences.instance.bulletImpactEffect,
            contact.point,
            Quaternion.LookRotation(contact.normal)
        );

        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }
}
