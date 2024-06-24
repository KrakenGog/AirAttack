using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody))]
public class Missile : MonoBehaviour
{
    public Rigidbody Target;

    public bool Thrusting => !_exploded && Time.time - _startTime < _config.LifeTime;

    private MissileConfig _config;
    [SerializeField] private VisualEffect _trailVfx;

    private float _startTime;

    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private Pool<Missile> _pool;
    private GameObject _view;
    private bool _exploded = false;

    public void Initialize(MissileConfig config, Pool<Missile> pool, Vector3 position)
    {
        if (_view == null)
        {
            _view = Instantiate(config.View);
            _view.transform.SetParent(transform, false);
            _view.transform.localPosition = Vector3.zero;

            _trailVfx = _view.GetComponentInChildren<VisualEffect>(true);
            _meshRenderer = _view.GetComponentInChildren<MeshRenderer>(true);
        }

        _startTime = Time.time;

        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();

        _config = config;
        _pool = pool;

        _rigidbody.MovePosition(position);

        _trailVfx.Play();
    }

    private void Update()
    {
        if (!Thrusting)
        {
            if (_trailVfx.transform.parent != null)
            {
                StartCoroutine(DestroyMissile());
            }

            return;
        }

        if (Target != null)
        {
            transform.RotateToXYZ(Target.position + Vector3.up * .5f - transform.position, _config.RotationSpeed, Time.deltaTime);
        }

        _rigidbody.velocity = transform.forward * _config.Speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_exploded)
            return;

        _exploded = true;
        GameplayStatics.SpawnExplosion(_config.Tnt, transform.position);

        if (_trailVfx.transform.parent != null)
        {
            _trailVfx.transform.parent = null;
            _trailVfx.Stop();
            Destroy(_trailVfx.gameObject, 10.0f);
        }

        Destroy(gameObject);
    }

    public void Activate()
    {
        if (_meshRenderer != null)
            _meshRenderer.enabled = true;
        if (_rigidbody != null)
            _rigidbody.isKinematic = false;
        if (_trailVfx != null)
            _trailVfx.Play();
    }

    private IEnumerator DestroyMissile()
    {

        _meshRenderer.enabled = false;
        _rigidbody.isKinematic = true;
        _trailVfx.Stop();

        yield return new WaitForSeconds(10);

        _pool.Push(this);
    }
}
