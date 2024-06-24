using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : PlaneState
{
    [SerializeField] private Path _path;
    [SerializeField] private float _threshold = .2f;

    private Vector3 _currentPathPoint;
    private int _index;

    public override void Initialize(GameSystemsRegistry gameSystemsRegistry, IStateSwitcher context)
    {
        base.Initialize(gameSystemsRegistry, context);

        _currentPathPoint = _path.GetFirstWaypoint();
    }

    public override void UpdateState(float delta)
    {
        
    }

    public override void PhysicsUpdateState(float delta)
    {
        _context.Plane.MoveForward(delta);

        Vector3 direction = _currentPathPoint - transform.position;

        Vector3 projectedDirection = new Vector3(direction.x, 0, direction.z);

        _context.Plane.Rotate(projectedDirection, delta);


        if (projectedDirection.magnitude < _threshold)
        {
            var (position, last) = _path.GetNextWaypoint(_index);

            if (last)
            {
                _currentPathPoint = _path.GetFirstWaypoint();
                _index = 0;
            }
            else
            {
                _currentPathPoint = position;
            }

        }
    }



}
