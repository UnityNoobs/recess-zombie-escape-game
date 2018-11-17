using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public string WeaponName;
    public GameObject bullet;
    public virtual Weapon OnPickup()
    {
        return gameObject.GetComponent<Weapon>();
    }

    public virtual void Fire(float direction, Transform location)
    {
        GameObject projectile = ObjectPool.instance.SpawnFromPool("bullets", location.position, location.rotation);
        projectile.GetComponent<BulletMovement>().direction = direction;
    }
}
