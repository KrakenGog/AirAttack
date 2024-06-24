using UnityEngine;

public abstract class MissileLauncher : MonoBehaviour
{
    [SerializeField] private Missile _missilePrefab;
    [SerializeField] protected MissileConfig _config;
    [SerializeField] private int _count;
    [SerializeField] private float _reload;
    [SerializeField] private float _reloadRandomRange = 0.5f;
    [SerializeField] private float _bulletSpeed;

    private float _currentTime;

    protected Pool<Missile> _pool;

    private float ReloadTime => _reload + (Random.value - 0.5f) * 2.0f * _reloadRandomRange;

    public void Initialize()
    {
        _currentTime = 0;

        _pool = new Pool<Missile>(GetInstance, Activate, Deactivate, 2);
    }

    private Missile GetInstance()
    {
        return Instantiate(_missilePrefab);
    }

    private void Deactivate(Missile missile)
    {
        missile.gameObject.SetActive(false);
    }

    private void Activate(Missile missile)
    {
        missile.Activate();

    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
    }

    public bool CanShoot()
    {
        return _currentTime <= 0 && _count > 0;
    }


    public virtual void Shoot(Rigidbody target)
    {
        _currentTime = ReloadTime;
        _count--;
    }
}
