using UnityEngine;

public class GroundVehicleMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private Rigidbody _rigidbody;

    public void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void MoveForward(float input, float delta)
    {
        _rigidbody.velocity = transform.forward.normalized * _speed;
    }

    public void RotateTo(Vector3 direction, float delta)
    {
        transform.RotateTo(direction, _rotationSpeed, delta);
    }
}
