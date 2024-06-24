using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTargets : GameObserver
{
    [SerializeField] private List<Health> _targets;

    public override string Description => "Destroy targets";

    public override void Initialize(GameSystemsRegistry gameSystemsRegistry)
    {
        base.Initialize(gameSystemsRegistry);

        _aim = _targets.Count;

        foreach (var target in _targets)
        {
            target.OnDeath += (_) =>
            {
                _progress++;
                CallChangedEvent();

                if(_progress == _aim) 
                    CallEvent();
            };
        }
    }
}
