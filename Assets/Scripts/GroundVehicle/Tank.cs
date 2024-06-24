using UnityEngine;

[RequireComponent(typeof(GroundVehicleMover))]
public class Tank : GroundVehicle
{
    public bool IsLookingAtTarget { get; private set; }

    [SerializeField] private GameObject _turret;
    [SerializeField] private float _turretRotationSpeed;
    [SerializeField] private float _turretAngleError = 3.0f;

    private GroundVehicleMover _mover;
    private BaseAttack _attack;

    public override void Initialize()
    {
        base.Initialize();

        _mover = GetComponent<GroundVehicleMover>();
        _attack = GetComponent<BaseAttack>();

        _mover.Initialize();
        _attack.Initialize();
    }

    public override void MoveForward(float input, float delta)
    {
        _mover.MoveForward(input, delta);
    }

    public override void RotateTo(Vector3 direction, float delta)
    {
        _mover.RotateTo(direction, delta);
    }

    public void RotateTurret(Vector3 target, float delta)
    {
        Vector3 direction = target - transform.position;

        _turret.transform.RotateTo(direction, _turretRotationSpeed, delta);

        IsLookingAtTarget = Mathf.Abs(Vector3.SignedAngle(_turret.transform.forward, direction, Vector3.up)) <= _turretAngleError;
    }

    public void Attack()
    {
        Debug.Log("attack 1");
        _attack.Attack();
    }

    public void Attack(Rigidbody target)
    {
        _attack.SetTarget(target);
        _attack.Attack();
    }
}
