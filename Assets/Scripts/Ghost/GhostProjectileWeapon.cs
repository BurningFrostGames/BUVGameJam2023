using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostProjectileWeapon : GhostWeapon
{
    public void SpawnProjectile()
    {
        var bullet = Instantiate(Projectile, spawnTransform.position, spawnTransform.rotation);
        bullet.AddForce(spawnTransform.up * speed);
        Destroy(bullet, 10f);
    }
}
