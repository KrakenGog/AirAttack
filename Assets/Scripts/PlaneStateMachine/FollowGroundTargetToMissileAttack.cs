using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class FollowGroundTargetToMissileAttack : PlaneState
{
    [SerializeField] private PlaneState _exitState;
    private GroundAIController _target;

    public void SetTarget(GroundAIController target)
    {
        _target = target;
    }

    public override void Enter()
    {
        if (_target == null)
        {
            _target = _context.FindGroundTarget();
        }
    }

    public override void UpdateState(float delta)
    {
        if (_target == null || _target.Dead)
            _switcher.SwithState(_exitState);
    }

    public override void PhysicsUpdateState(float delta)
    {
        _context.Plane.MoveForward(delta);

        Vector3 direction = _target.transform.position - transform.position;

        Vector3 projectedDirection = new Vector3(direction.x, 0, direction.z);

        float distance = Vector3.Distance(_target.transform.position, transform.position);

        float angle = _context.Plane.Rotate(projectedDirection, delta);

        if(angle < 50 && distance < 200)
        {
            if (_context.Plane.TryLaunchMissile(_target.Vehicle.Rigidbody))
            {
                _target = _context.FindGroundTarget(_target);
            }
            else
            {
                _switcher.SwithState<FollowPath>();
            }
        }
    }
}
