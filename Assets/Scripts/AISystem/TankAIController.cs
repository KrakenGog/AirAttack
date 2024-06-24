using UnityEngine;

public class TankAIController : GroundAIController
{
    [SerializeField] private float _angleMovingThreshold = 20.0f;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _raycastStart;

    private Vector3 _lastWaypoint;
    private EntitiesRegistry _entitiesRegistry;

    public override void Initialize(GameSystemsRegistry gameSystemRegistry)
    {
        base.Initialize(gameSystemRegistry);

        _entitiesRegistry = gameSystemRegistry.GetSystem<EntitiesRegistry>();
    }

    protected override bool ShouldMoveForward(Vector3 waypoint)
    {
        _lastWaypoint = waypoint;
        return Mathf.Abs(Vector3.SignedAngle(transform.forward,
            Vector3.ProjectOnPlane(waypoint - Vehicle.transform.position, Vector3.up).normalized,
            Vector3.up)) <= _angleMovingThreshold;
    }

    protected override void UpdateAI()
    {
        if (State == AIState.Dead) return;

        var tank = (Tank)Vehicle;

        if (ShouldMoveForward(_lastWaypoint))
            State = AIState.Moving;


        if (_behavior == AIBehavior.MoveAndFindTarget)
        {
            _target = null;

            GroundAIController target = FindTarget();

            if (target != null)
                _target = target.transform;

            if (_target == null)
                return;

            tank.RotateTurret(_target.position, Time.deltaTime);

            if (tank.IsLookingAtTarget)
                tank.Attack(_target.GetComponent<Rigidbody>());
        }

    }

    private bool IsAttackPathObscuredByObstacle(Transform target)
    {
        if (Physics.Raycast(_raycastStart.position, target.position - _raycastStart.position, out RaycastHit hit))
        {
            if (hit.collider.attachedRigidbody != null && hit.collider.attachedRigidbody.TryGetComponent(out GroundAIController controller))
            {
                if (controller.Team != _team)
                    return false;
            }
        }

        return true;
    }

    private GroundAIController FindTarget()
    {
        GroundAIController target = null;
        float minDistance = Mathf.Infinity;

        foreach (var enemy in _entitiesRegistry.GetOtherTeam(_team))
        {
            if (enemy.Dead || IsAttackPathObscuredByObstacle(enemy.transform)) continue;

            float distance = Vector3.Distance(enemy.transform.position, transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                target = enemy;
            }
        }

        return target;
    }
}
