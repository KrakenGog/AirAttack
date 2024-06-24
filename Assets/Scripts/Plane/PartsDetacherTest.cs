using UnityEngine;

public class PartsDetacherTest : MonoBehaviour
{
    [SerializeField] private Rigidbody Plane;
    [SerializeField] private Transform Target;
    [SerializeField] private GameObject ParticlesPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log($"Detaching {Target.name}");

            Target.parent = null;

            Target.gameObject.AddComponent<Rigidbody>().velocity = Plane.velocity;
            Target.gameObject.GetComponent<Collider>().enabled = true;
            Target.gameObject.layer = 7;

            var particles = Instantiate(ParticlesPrefab, Target.position, Quaternion.identity);
            particles.transform.SetParent(Target.transform);
        }
    }
}
