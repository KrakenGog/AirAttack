using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendTargets : GameObserver
{
    [SerializeField] private List<Health> _targets;

    public override string Description => "Defend targets";

    public override void Initialize(GameSystemsRegistry gameSystemsRegistry)
    {
        base.Initialize(gameSystemsRegistry);

        _aim = _targets.Count;
        _progress = _aim;

        foreach (var target in _targets)
        {
            target.OnDeath += (_) =>
            {
                _progress--;

                CallChangedEvent();

                if (_progress == 0)
                    CallEvent();
            };

        }
    }
}
