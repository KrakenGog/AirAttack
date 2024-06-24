using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Health))]
public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject _undestroyed;
    [SerializeField] private GameObject _destroyed;

    [SerializeField] private VisualEffectAsset _smokeParticles;
    [SerializeField] private float _smokeLifeTime = 10.0f;
    [SerializeField] private Vector3 _particlesScale = Vector3.one;

    private void Start()
    {
        GetComponent<Health>().OnDeath += OnDeath;
    }

    private void OnDeath(Component instigator)
    {
        _undestroyed.SetActive(false);
        _destroyed.SetActive(true);

        var vfx = GameplayStatics.SpawnVfx(_smokeParticles, transform.position, _smokeLifeTime);

        vfx.position = _destroyed.transform.position;
        vfx.rotation = transform.rotation;
        vfx.localScale = _particlesScale;
    }
}
