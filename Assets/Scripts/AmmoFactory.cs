using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Serialization;

public class AmmoFactory : MonoBehaviour
{
    public Transform spawnTransform;
    public float speed = 10f;
    public float fireRate = 10f;

    private float _nextTimeToFire;

    private Ghost _ghost;
    private GhostCatcher _catcher;

    public void Init(GhostCatcher catcher)
    {
        _catcher = catcher;
    }
    
    public void StartFactory(Ghost ghost)
    {
        _ghost = ghost;
        enabled = true;
    }

    public void StopFactory()
    {
        _ghost = null;
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_ghost) return;

        if (_ghost.CurrentAmmo <= 0)
        {
            _catcher.EjectGhost();
            return;
        }
        
        if (Input.GetMouseButton(0))
        {
            if (!(Time.time >= _nextTimeToFire)) return;

            SpawnProjectile();
            _ghost.ConsumeAmmo();

            MMEventManager.TriggerEvent(new GhostParameter
            {
                Ghost = _ghost
            });

            _nextTimeToFire = Time.time + 1f / fireRate;
        }
    }

    public void SpawnProjectile()
    {
        var bullet = Instantiate(_ghost.Projectile, spawnTransform.position, spawnTransform.rotation);
        bullet.AddForce(spawnTransform.up * speed);
        Destroy(bullet, 10f);
    }
}
