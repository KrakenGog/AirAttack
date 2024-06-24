using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMissileLauncher : MissileLauncher
{
    public override void Shoot(Rigidbody target)
    {
        base.Shoot(target);

        Missile missile = _pool.Get();
        missile.Initialize(_config, _pool, transform.position);

        missile.transform.forward = target.transform.position - transform.position;
    }
}
