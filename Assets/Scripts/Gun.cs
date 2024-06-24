using UnityEngine;
using UnityEngine.VFX;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private BulletConfig _bulletConfig;
    [SerializeField] private float _reload;
    [SerializeField] private float _reloadRandomRange = 0.5f;
    [SerializeField] private float _bulletSpeed;

    [SerializeField] private VisualEffectAsset _vfx;
    [SerializeField] private float _vfxLifeTime = 0.5f;

    private float _currentTime;

    private Pool<Bullet> _pool;

    private float ReloadTime => _reload + (Random.value - 0.5f) * 2.0f * _reloadRandomRange;

    public void Initialize()
    {
        _currentTime = 0;

        _pool = new Pool<Bullet>(GetInstance, Activate, Deactivate, 2);
    }

    private Bullet GetInstance()
    {
        return Instantiate(_bulletPrefab);
    }

    private void Deactivate(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void Activate(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.ResetLifeTime();
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
    }

    public bool CanShoot()
    {
        return _currentTime <= 0;
    }


    public void Shoot()
    {
        Bullet bullet = _pool.Get();

        bullet.Initialize(_bulletConfig, transform.forward.normalized * _bulletSpeed, transform.position, _pool);
        if (_vfx != null) GameplayStatics.SpawnVfx(_vfx, transform.position, transform.forward, _vfxLifeTime);

        _currentTime = ReloadTime;
    }

    public void Shoot(float lifeTime)
    {
        Bullet bullet = _pool.Get();

        bullet.Initialize(_bulletConfig, transform.forward.normalized * _bulletSpeed, transform.position, _pool);

        bullet.SetLifeTime(lifeTime);

        _currentTime = ReloadTime;
    }
}
