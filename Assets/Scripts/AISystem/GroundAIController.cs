using UnityEngine;

public enum AIState
{
    Dead, Idling, Moving
}

[RequireComponent(typeof(GroundVehicle))]
public abstract class GroundAIController : MonoBehaviour
{
    protected AIState State = AIState.Moving;
    public GroundVehicle Vehicle => _vehicle;
    public bool Dead => State == AIState.Dead;

    [SerializeField] private Path _path;
    [SerializeField] protected Team _team;
    [SerializeField] protected AIBehavior _behavior;
    [SerializeField] private float _waypointReachRadius = 0.5f;

    private GroundVehicle _vehicle;


    private Vector3 _waypoint;
    private int _waypointIndex;
    private bool _reachedEndOfPath = false;
    private bool _thisWaypointIsLast = false;
    private float _reachRadiusSquared;

    public Team Team => _team;

    public virtual void Initialize(GameSystemsRegistry gameSystemRegistry)
    {
        _vehicle = GetComponent<GroundVehicle>();

        _waypoint = _path.GetFirstWaypoint();
        _waypointIndex = 0;

        _reachRadiusSquared = _waypointReachRadius * _waypointReachRadius;

        _vehicle.Initialize();

        _vehicle.Health.OnDeath += (_) =>
        {
            State = AIState.Dead;
        };
    }


    private void FixedUpdate()
    {
        UpdateAI();

        if (_reachedEndOfPath || State == AIState.Dead) return;

        var direction = Vector3.ProjectOnPlane(_waypoint - _vehicle.transform.position, Vector3.up).normalized;

        if (State == AIState.Idling || State == AIState.Moving) _vehicle.RotateTo(direction, Time.deltaTime);
        if (State == AIState.Moving && ShouldMoveForward(_waypoint))
        {
            _vehicle.MoveForward(1.0f, Time.deltaTime);
            State = AIState.Moving;
        }
        else
        {
            State = AIState.Idling;
        }

        if (Vector3.ProjectOnPlane(_vehicle.transform.position - _waypoint, Vector3.up).sqrMagnitude <= _reachRadiusSquared)
        {
            if (_thisWaypointIsLast)
            {
                _reachedEndOfPath = true;
                State = AIState.Idling;
            }
            else
                (_waypoint, _thisWaypointIsLast) = _path.GetNextWaypoint(_waypointIndex++);
        }
    }

    protected virtual bool ShouldMoveForward(Vector3 waypoint) => true;

    protected virtual void UpdateAI() { }
    protected virtual void OnDeath() { }
}

public enum AIBehavior
{
    Stay,
    MoveOnPath,
    MoveAndFindTarget
}
