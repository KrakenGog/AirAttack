using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class VFXRecycler : MonoBehaviour
{
    [SerializeField] private float _maxLifeTime;

    public bool Active => gameObject.activeSelf;

    private VisualEffect _effect;

    private Pool<VFXRecycler> _originPool;

    private Coroutine _current;

    //private bool _checkState;

    private float _time;

    public void Initialize(Pool<VFXRecycler> pool)
    {
        _originPool = pool;
        _effect = GetComponentInChildren<VisualEffect>();
        Recycle();
    }

    public void Initialize(Pool<VFXRecycler> pool, VisualEffectAsset effect, float lifeTime)
    {
        _originPool = pool;
        _maxLifeTime = lifeTime;
        _effect = GetComponentInChildren<VisualEffect>();
        _effect.visualEffectAsset = effect;

        Recycle();
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _maxLifeTime)
        {
            TryPush();
        }
    }

    public void Recycle()
    {
        _time = 0;
        gameObject.SetActive(true);
        if (_effect != null)
        {
            _effect.Play();
            StartCoroutine(WatchVFXState());
        }
    }

    public void Deactivate()
    {
        if (_effect != null)
            _effect.Stop();
        gameObject.SetActive(false);
    }





    private IEnumerator WatchVFXState()
    {
        yield return new WaitForSeconds(.1f);



        while (_effect.HasAnySystemAwake())
        {
            yield return new WaitForSeconds(0.1f);
        }

        TryPush();


    }

    private void TryPush()
    {
        if (_originPool != null)
            _originPool.Push(this);
        else
            Destroy(gameObject);
    }
}
