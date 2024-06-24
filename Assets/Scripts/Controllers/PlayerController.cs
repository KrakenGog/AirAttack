using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public event UnityAction PlayerDied;

    private Plane _plane;

    private InputSystem _inputSystem;

    public bool Dead => _health.Dead;
    public Rigidbody Rigidbody => _plane.Rigidbody;

    private Health _health;

    public void Initialize(InputSystem inputSystem)
    {
        _plane = GetComponent<Plane>();

        _plane.Initialize();

        _inputSystem = inputSystem;

        _inputSystem.LaunchBomb += LaunchBomb;
        _inputSystem.GunAttack += GunAttack;

        _health = GetComponent<Health>();
        _health.OnDeath += (i) =>
        {
            _inputSystem.LaunchBomb -= LaunchBomb;
            _inputSystem.GunAttack -= GunAttack;

            PlayerDied?.Invoke();
        };
    }

    private void GunAttack()
    {
        _plane.GunAttack();
    }

    private void LaunchBomb()
    {
        _plane.TryLaunchBomb();
    }

    private void FixedUpdate()
    {
        if (_health.Dead) return;

        _plane.MoveForward(Time.fixedDeltaTime);

        _plane.Rotate(_inputSystem.RotationDirection, Time.fixedDeltaTime);
    }
}
