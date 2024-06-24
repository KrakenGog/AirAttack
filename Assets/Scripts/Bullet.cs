using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _damage;
    private Vector3 _speed;
    private float _lifeTime;
    private float _currentLifeTime;

    private Pool<Bullet> _originPool;

    private Rigidbody _rigidbody;
    private GameObject _view;

    public void Initialize(BulletConfig bulletConfig, Vector3 startSpeed, Vector3 startPosition, Pool<Bullet> pool)
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.MovePosition(startPosition);

        if (_view == null)
        {
            _view = Instantiate(bulletConfig.View);
            _view.transform.SetParent(transform);
            _view.transform.localPosition = Vector3.zero;
        }



        _view.transform.forward = startSpeed;

        _damage = bulletConfig.Damage;

        _speed = startSpeed;

        _lifeTime = bulletConfig.LifeTime;

        _originPool = pool;

    }

    private void Update()
    {
        _currentLifeTime += Time.deltaTime;

        if (_currentLifeTime >= _lifeTime)
            Recylce();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _speed;
    }

    public void SetLifeTime(float time)
    {
        _lifeTime = time;
    }

    public void ResetLifeTime()
    {
        _currentLifeTime = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetHealth(out var health))
        {
            health.TakeDamage(this, _damage);
            Recylce();
        }
    }

    private void Recylce()
    {
        _originPool.Push(this);
    }
}
