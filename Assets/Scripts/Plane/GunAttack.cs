using System.Collections.Generic;
using UnityEngine;

public class GunAttack : BaseAttack
{
    [SerializeField] private List<Gun> _guns;

    public override void Initialize()
    {
        foreach (var gun in _guns)
        {
            gun.Initialize();
        }
    }

    public override bool CanAttack()
    {
        foreach (var gun in _guns)
        {
            if (gun.CanShoot())
                return true;
        }

        return false;
    }

    public override void Attack()
    {
        foreach (var gun in _guns)
        {
            if (gun.CanShoot())
                ShootGun(gun);
        }
    }

    private void ShootGun(Gun gun)
    {
        if (_target != null)
            gun.transform.forward = _target.transform.position - gun.transform.position;
        gun.Shoot();
    }
}
