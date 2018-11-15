using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWeapon : Weapon {

    public override void Fire(float direction, Transform location)
    {
        //base.Fire(direction, location);
        GameObject projectile = ObjectPool.instance.SpawnFromPool("BigBullets", location.position, location.rotation);
        projectile.GetComponent<BulletMovement>().direction = direction;
    }
}
