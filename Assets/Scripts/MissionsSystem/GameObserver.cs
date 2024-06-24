using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameObserver : MonoBehaviour
{
    [SerializeField] private bool _render = true;

    protected GameSystemsRegistry _gameSystemsRegistry;
    protected float _progress;
    protected float _aim;

    public bool Render => _render;
    public float Progress => _progress;
    public float Aim => _aim;
    public virtual string Description { get; }

    public event UnityAction<GameObserver> OnReached;
    public event UnityAction<GameObserver> OnChanged;

    public virtual void Initialize(GameSystemsRegistry gameSystemsRegistry)
    {
        _gameSystemsRegistry = gameSystemsRegistry;
    }

    protected void CallEvent()
    {
        OnReached?.Invoke(this);
    }

    protected void CallChangedEvent()
    {
        OnChanged?.Invoke(this);
    }
}
