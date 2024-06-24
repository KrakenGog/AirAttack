using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float _tnt;
    private float _timer;

    private Coroutine _current;


    public bool Activated => _current != null;

    public void Initialize(BombConfig bombConfig, float timer, Vector3 linearVelocity, Vector3 angularVelocity, Vector3 startPosition)
    {
        GameObject view = Instantiate(bombConfig.View);
        view.transform.SetParent(transform);
        view.transform.localPosition = Vector3.zero;

        _tnt = bombConfig.Tnt;
        _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.velocity = linearVelocity;
        _rigidbody.angularVelocity = angularVelocity;

        _rigidbody.MovePosition(startPosition);

        _timer = timer;
    }

    private void FixedUpdate()
    {
        if (_current == null)
            transform.forward = _rigidbody.velocity;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!Activated)
        {
            _current = StartCoroutine(BombTimer());
        }
    }

    private IEnumerator BombTimer()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForSeconds(_timer);

        Destroy(gameObject);

        GameplayStatics.SpawnExplosion(_tnt, transform.position);
    }

}
