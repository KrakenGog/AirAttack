using UnityEngine;

public class CameraMover : GameSystem
{
    [SerializeField] private float _cameraHeight;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _smoothTime;
    [SerializeField] private float _velocityInfluence;
    [SerializeField] private float _rotationMultyplier;

    [SerializeField] private Vector3 _currentVelocity;

    private Vector3 _lastPosition;

    public void MoveToTarget(Transform target, Vector3 targetVelocity)
    {
        Vector3 newPosition = Vector3.SmoothDamp(_camera.position, target.position + targetVelocity * _velocityInfluence, ref _currentVelocity, _smoothTime);

        newPosition = new Vector3(newPosition.x, _cameraHeight, newPosition.z);

        _camera.position = newPosition;

        //Vector3 cameraVeloctity = _camera.position - _lastPosition;

        //float xAngle = cameraVeloctity.normalized.z * _rotationMultyplier;
        //float zAngle = cameraVeloctity.normalized.x * _rotationMultyplier;

        //_camera.rotation = Quaternion.Euler(xAngle, 0, zAngle);

        //_lastPosition = _camera.position;
    }


}
