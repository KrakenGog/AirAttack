using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public abstract class State : MonoBehaviour
{
    protected IStateSwitcher _switcher;
    protected GameSystemsRegistry _gameSystemsRegistry;

    public virtual void Initialize(GameSystemsRegistry gameSystemsRegistry, IStateSwitcher context)
    {
        _switcher = context;
        _gameSystemsRegistry = gameSystemsRegistry;
    }

    public virtual void Enter() { }
    public virtual void UpdateState(float delta) { }
    public virtual void PhysicsUpdateState(float delta) { }
    public virtual void Exit() { }

    public virtual void Stop() { }
    public virtual void Continue() { }
}


