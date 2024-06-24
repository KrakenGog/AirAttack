using UnityEngine;

public class TestController : MonoBehaviour
{
    [SerializeField] private Plane _plane;
    [SerializeField] private CameraMover _cameraMover;

    private float _input;
    private float _rotationInput;

    private void Start()
    {
        _plane.Initialize();
    }

    private void FixedUpdate()
    {

        _plane.MoveForward(Time.fixedDeltaTime);


        _plane.Rotate(_rotationInput, Time.fixedDeltaTime);
    }

    private void Update()
    {
        _cameraMover.MoveToTarget(_plane.transform, _plane.Rigidbody.velocity);

        _input = Input.GetKey(KeyCode.W) ? 1 : 0;
        _rotationInput = 0;

        if (Input.GetKey(KeyCode.A))
        {
            _rotationInput = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _rotationInput = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _plane.TryLaunchBomb();
        }

    }
}
