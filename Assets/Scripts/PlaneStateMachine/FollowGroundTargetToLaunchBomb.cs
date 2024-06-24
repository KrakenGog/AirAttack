using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public class FollowGroundTargetToLaunchBomb : PlaneState
{
    [SerializeField] private PlaneState _exitState;
    private GroundAIController _target;

    public void SetTarget(GroundAIController target)
    {
        _target = target;
    }

    public override void Enter()
    {
        if(_target == null )
        {
            _target = _context.FindGroundTarget();
        }
    }

    public override void UpdateState(float delta)
    {
        if (_target.Dead)
            _switcher.SwithState(_exitState);
    }

    public override void PhysicsUpdateState(float delta)
    {
        _context.Plane.MoveForward(delta);

        var (position, normal, time) = _context.Plane.BombAttack.CaclulateBombTrajectory();

        Vector3 direction = _target.transform.position + _target.Vehicle.Rigidbody.velocity * time - transform.position;

        Vector3 projectedDirection = new Vector3(direction.x, 0, direction.z);

        float distance = Vector3.Distance(_target.transform.position + _target.Vehicle.Rigidbody.velocity * time, position);

        float currentAngle = _context.Plane.Rotate(projectedDirection, delta);

        if (currentAngle < 10 && distance < 5)
        {
            if (_context.Plane.TryLaunchBomb())
            {
                _target = _context.FindGroundTarget(_target);
            }
            else
            {
                _switcher.SwithState<FollowPath>();
            }
        }
       // else if ()

    }
}
