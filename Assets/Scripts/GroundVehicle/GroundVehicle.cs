using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Health))]
public class GroundVehicle : MonoBehaviour
{
    [SerializeField] private VisualEffectAsset _deathVfx;

    protected Health _health;
    protected Rigidbody _rigidbody;

    public Health Health => _health;
    public Rigidbody Rigidbody => _rigidbody;
    public bool Dead => _health.Dead;

    public virtual void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _health = GetComponent<Health>();

        _health.OnDeath += (_) =>
        {
            GameplayStatics.SpawnVfx(_deathVfx, transform.position, 5);
            Debug.LogWarning($"TODO: Add pool to {nameof(GroundVehicle)}");
        };
    }

    public virtual void MoveForward(float input, float delta) { }
    public virtual void RotateTo(Vector3 direction, float delta) { }
}
