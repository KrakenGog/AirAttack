using UnityEngine;
using UnityEngine.Events;

public abstract class LoseCondition : MonoBehaviour
{
    protected GameSystemsRegistry _gameSystemsRegistry;

    public event UnityAction<LoseCondition> OnLoos;

    public virtual void Initialize(GameSystemsRegistry gameSystemsRegistry)
    {
        _gameSystemsRegistry = gameSystemsRegistry;
    }

    protected void LoosEvent()
    {
        OnLoos?.Invoke(this);
    }
}
