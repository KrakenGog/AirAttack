using UnityEngine;

public class StaticAntiAirDefender : GroundVehicle
{
    [SerializeField] private GameObject _view;
    [SerializeField] private AntiAirAttack _antiAirAttack;
    [SerializeField] private float _range;
    [SerializeField] private float _accuracy;
    [SerializeField] private float _maxProjectileInaccuracyRange = 25.0f;


    public override void Initialize()
    {
        base.Initialize();



        _antiAirAttack.Initialize();
    }



    public void Attack(Rigidbody target)
    {
        if (Vector3.Distance(_view.transform.position, target.transform.position) > _range)
            return;

        var (position, time) = TargetPositionPredictor.Predict(target.transform.position, target.velocity, _view.transform.position, 280);
        position += Random.onUnitSphere * (1.0f - _accuracy) * _maxProjectileInaccuracyRange;

        RotateToTarget(position);

        _antiAirAttack.Attack(time, position);
    }

    public void RotateToTarget(Vector3 target)
    {
        _view.transform.forward = (target - transform.position).normalized;
    }
}
