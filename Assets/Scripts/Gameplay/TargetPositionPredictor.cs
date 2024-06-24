using UnityEngine;

public static class TargetPositionPredictor
{
    public static (Vector3 position, float time) Predict(Vector3 targetPosition, Vector3 targetVelocity,
        Vector3 projectileStartPosition, float projectileStartSpeed)
    {
        float time = Mathf.Abs((projectileStartPosition - targetPosition).magnitude / (targetVelocity.magnitude - projectileStartSpeed));

        Vector3 targetPos = targetPosition + targetVelocity * time;
        return (targetPos, time);
    }
}
