using UnityEngine;

public class GroundVehicleView : MonoBehaviour
{
    [SerializeField] private Material _destroyedMaterial;
    [SerializeField] private MeshRenderer[] _meshRenderers;

    private void Start()
    {
        GetComponent<Health>().OnDeath += (_) => OnDeath();
    }

    protected virtual void OnDeath()
    {
        if (_destroyedMaterial != null)
        {
            foreach (var renderer in _meshRenderers)
            {
                renderer.material = _destroyedMaterial;
            }
        }
    }
}
