using UnityEngine;

public class TrajectoryCalculator : MonoBehaviour
{
    [HideInInspector] public Vector3 StartPosition;
    [HideInInspector] public Vector3 StartVelocity;

    [SerializeField] private float _simulationTimeStep = 0.1f;
    [SerializeField] private int _maxSimulationSteps = 50;
    [SerializeField] private LayerMask _groundLayer;

    public (Vector3 position, Vector3 normal, float time) Calculate()
    {
        Vector3 prevPosition = transform.position;

        float time = 0.0f;

        for (int i = 0; i < _maxSimulationSteps; i++)
        {
            time += _simulationTimeStep;

            Vector3 nextPosition = new Vector3(
                StartPosition.x + StartVelocity.x * time,
                StartPosition.y + Physics.gravity.y * time * time / 2.0f,
                StartPosition.z + StartVelocity.z * time
                );

            if (Physics.Raycast(prevPosition, -(prevPosition - nextPosition).normalized, out var hitInfo, (prevPosition - nextPosition).magnitude, _groundLayer))
            {
                return (hitInfo.point, hitInfo.normal, time);
            }

            prevPosition = nextPosition;
        }

        return (Vector3.zero, Vector3.up, 0);
    }
}
