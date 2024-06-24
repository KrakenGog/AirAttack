using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlaneAIController : AirAIController, IStateSwitcher
{
    [SerializeField] private Team _team;
    [SerializeField] private State _startState;

    private Plane _plane;
    private EntitiesRegistry _entitiesRegistry;

    public Plane Plane => _plane;

    [SerializeField] private State _currentState;

    private List<PlaneState> _planeStates;

    public void Initialize(GameSystemsRegistry gameSystemsRegistry)
    {
        _plane = GetComponent<Plane>();
        _entitiesRegistry = gameSystemsRegistry.GetSystem<EntitiesRegistry>();
        _planeStates = GetComponents<PlaneState>().ToList();

        _plane.Initialize();

        foreach (var state in _planeStates)
        {
            state.Initialize(gameSystemsRegistry, this);
        }

        _currentState = _startState;
        _currentState.Enter();

    }

    private void Update()
    {
        _currentState.UpdateState(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _currentState.PhysicsUpdateState(Time.deltaTime);
    }



    public GroundAIController FindGroundTarget()
    {
        GroundAIController target = null;
        float misDistance = Mathf.Infinity;

        foreach (GroundAIController controller in _entitiesRegistry.GetOtherTeam(_team))
        {
            if (controller.Dead)
                continue;

            float distance = Vector3.Distance(transform.position, controller.transform.position);

            if(distance < misDistance)
            {
                misDistance = distance;
                target = controller;
            }
        }

        return target;
    }

    public GroundAIController FindGroundTarget(GroundAIController exclude)
    {
        GroundAIController target = null;
        float misDistance = Mathf.Infinity;

        foreach (GroundAIController controller in _entitiesRegistry.GetOtherTeam(_team))
        {
            if (controller == exclude || controller.Dead)
                continue;

            float distance = Vector3.Distance(transform.position, controller.transform.position);

            if (distance < misDistance)
            {
                misDistance = distance;
                target = controller;
            }
        }

        return target;
    }

    public T SwithState<T>() where T : State
    {
        _currentState.Exit();
        _currentState = _planeStates.Find((state) => state is  T);
        _currentState.Enter();

        return (T)_currentState;
    }

    public T SwithState<T>(T state) where T : State
    {
        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();

        return (T)_currentState;
    }
}
