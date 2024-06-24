using UnityEngine;
using UnityEngine.Events;

public abstract class MissionAim : MonoBehaviour
{
    protected GameSystemsRegistry _gameSystemsRegistry;
    protected float _progress;
    protected float _aim;


    public float Progress => _progress;
    public float Aim => _aim;
    public virtual string Description { get;}

    public event UnityAction<MissionAim> OnReached;
    public event UnityAction<MissionAim> OnChanged;

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
