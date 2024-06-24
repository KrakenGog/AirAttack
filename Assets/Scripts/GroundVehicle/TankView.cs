using Unity.VisualScripting;
using UnityEngine;

public class TankView : GroundVehicleView
{
    [SerializeField] private Transform _turret;
    [SerializeField] private float _turretLaunchChance = 0.1f;
    [SerializeField] private Vector2 _launchForceBounds = new Vector2(30.0f, 100.0f);
    [SerializeField] private float _launchForceNoise = 1.0f;

    protected override void OnDeath()
    {
        base.OnDeath();

        if (Random.value < _turretLaunchChance)
        {
            _turret.AddComponent<Rigidbody>().AddForce(transform.up * Random.Range(_launchForceBounds.x, _launchForceBounds.y)
                + new Vector3(Random.Range(-_launchForceNoise, _launchForceNoise), 0.0f, Random.Range(-_launchForceNoise, _launchForceNoise)),
                ForceMode.VelocityChange);
        }
    }
}
