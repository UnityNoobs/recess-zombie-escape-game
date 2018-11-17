using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongWeapon : Weapon {
    public override void Fire(float direction, Transform location)
    {
        //base.Fire(direction, location);
        GameObject projectile = ObjectPool.instance.SpawnFromPool("bullets", location.position, location.rotation);
        projectile.GetComponent<BulletMovement>().direction = direction;
        projectile = ObjectPool.instance.SpawnFromPool("bullets", location.position, location.rotation);
        projectile.GetComponent<BulletMovement>().direction = direction * -1;
    }
}
