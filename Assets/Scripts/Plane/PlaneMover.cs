using UnityEngine;

public class PlaneMover : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _rotationSpeed;

    public float RotationInput { get; private set; }

    private Rigidbody _rigidbody;

    public Rigidbody Rigidbody => _rigidbody;

    public float MaxSpeed => _maxSpeed;
    public float RotationSpeed => _rotationSpeed;

    public void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void MoveForward(float delta)
    {
        //Vector3 forward = transform.forward;
        //Vector3 velocity = _rigidbody.velocity;

        //Vector3 forwardSpeed = new Vector3(forward.x * velocity.x, forward.y * velocity.y, forward.z * velocity.z);

        //if (forwardSpeed.magnitude < _maxSpeed)
        _rigidbody.velocity = transform.forward.normalized * _maxSpeed;
    }

    public float Rotate(float input, float delta)
    {
        transform.Rotate(Vector3.up * input * _rotationSpeed * delta);
        RotationInput = input;

        return 0;
    }

    public float Rotate(Vector3 direction, float delta)
    {
        float angle = Vector3.SignedAngle(transform.forward.normalized, direction, Vector3.up);

        RotationInput = Mathf.Clamp(angle, -1, 1);

        Quaternion target = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y + angle, transform.localEulerAngles.z);

        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, target, _rotationSpeed * delta);

        return Vector3.SignedAngle(transform.forward.normalized, direction, Vector3.up);
    }

    public void HandleDeath()
    {
        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.drag = 0.0f;
    }
}
