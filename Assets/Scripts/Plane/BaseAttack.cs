using UnityEngine;

public abstract class BaseAttack : MonoBehaviour
{
    protected Rigidbody _target;
    public virtual void SetTarget(Rigidbody target)
    {
        _target = target;
    }
    public virtual void Initialize() { }

    public abstract bool CanAttack();

    public abstract void Attack();
}
