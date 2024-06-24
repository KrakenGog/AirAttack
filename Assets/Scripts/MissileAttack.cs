using System.Collections.Generic;
using UnityEngine;

public class MissileAttack : BaseAttack
{
    [SerializeField] private List<MissileLauncher> _missileLaunchers;

    public override void Initialize()
    {
        foreach (MissileLauncher launcher in _missileLaunchers)
        {
            launcher.Initialize();
        }
    }

    public override void Attack()
    {
        foreach (MissileLauncher launcher in _missileLaunchers)
        {
            if (launcher.CanShoot())
                launcher.Shoot(_target);
        }
    }

    public override bool CanAttack()
    {
        foreach (MissileLauncher launcher in _missileLaunchers)
        {
            if (launcher.CanShoot())
                return true;
        }

        return false;
    }
}
