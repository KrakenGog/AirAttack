using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathAndFindTarget : FollowPath
{
    [SerializeField] private PlaneState _exitState;
    public override void UpdateState(float delta)
    {
        base.UpdateState(delta);

        GroundAIController target = _context.FindGroundTarget();

        if (target != null)
        {
            _context.SwithState(_exitState);
        }
    }
}
