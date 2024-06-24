using UnityEngine;

public class Plane : MonoBehaviour
{
    private PlaneMover _planeMover;
    private PlaneView _planeView;
    private Health _health;
    private BombAttack _bombAttack;
    private GunAttack _gunAttack;
    private MissileAttack _missileAttack;
    private TrajectoryCalculator _trajectoryCalculator;


    public Rigidbody Rigidbody => _planeMover.Rigidbody;
    public BombAttack BombAttack => _bombAttack;

    public float RotationSpeed => _planeMover.RotationSpeed;

    public void Initialize()
    {
        _planeMover = GetComponent<PlaneMover>();
        _planeView = GetComponent<PlaneView>();
        _health = GetComponent<Health>();

        _trajectoryCalculator = GetComponent<TrajectoryCalculator>();

        _planeMover.Initialize();

        if (_planeView != null)
            _planeView.Initialize(_planeMover, _health);

        if (TryGetComponent(out BombAttack bombAttack))
        {
            _bombAttack = bombAttack;

            _bombAttack.Initialize(_trajectoryCalculator);
        }

        if (TryGetComponent(out GunAttack gunAttack))
        {
            _gunAttack = gunAttack;

            _gunAttack.Initialize();
        }

        if(TryGetComponent(out MissileAttack missile))
        {
            _missileAttack = missile;

            _missileAttack.Initialize();
        }


        _health.OnDeath += (i) =>
        {
            _planeMover.HandleDeath();
        };
    }

    public bool TryLaunchBomb()
    {
        if (_bombAttack == null || !_bombAttack.CanLaunchBomb())
            return false;

        _bombAttack.LaunchBomb();

        return true;
    }

    public bool TryLaunchMissile(Rigidbody target)
    {
        if (!_missileAttack.CanAttack())
            return false;


        _missileAttack.SetTarget(target);
        _missileAttack.Attack();

        return true;
    }

    public void GunAttack()
    {
        _gunAttack.Attack();
    }


    public void MoveForward(float delta)
    {
        _planeMover.MoveForward(delta);
    }

    public void Rotate(float input, float delta)
    {
        _planeMover.Rotate(input, delta);
    }

    public float Rotate(Vector3 direction, float delta)
    {
        return _planeMover.Rotate(direction, delta);
    }
}
