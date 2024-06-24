using UnityEngine;

public static class Extensions
{
    public static void RotateTo(this Transform transform, Vector3 direction, float speed, float delta)
    {
        float angle = Vector3.SignedAngle(transform.forward.normalized, direction, Vector3.up);

        Quaternion target = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y + angle, transform.localEulerAngles.z);

        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, target, speed * delta);
    }

    public static void RotateToXYZ(this Transform transform, Vector3 direction, float speed, float delta)
    {
        Quaternion target = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, speed * delta);
    }

    public static bool GetHealth(this Component component, out Health health)
    {
        health = null;
        try
        {
            health = component.gameObject.GetComponentInParent<Health>(true);
        }
        catch { }

        return health != null;
    }

    public static bool GetHealth(this Collider collider, out Health health)
    {
        if (collider.attachedRigidbody == null)
        {
            health = null;
            return false;
        }

        return collider.attachedRigidbody.gameObject.TryGetComponent(out health);
    }

    public static void TakeDamage(this Collider collider, float damage)
    {
        if (collider.GetHealth(out var health))
        {
            health.TakeDamage(collider, damage);
        }
    }
}
