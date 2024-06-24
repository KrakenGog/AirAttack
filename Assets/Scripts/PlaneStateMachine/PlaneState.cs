using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlaneState : State
{
    protected PlaneAIController _context;

    public override void Initialize(GameSystemsRegistry gameSystemsRegistry, IStateSwitcher context)
    {
        base.Initialize(gameSystemsRegistry, context);

        _context = (PlaneAIController) context;
    }


}
