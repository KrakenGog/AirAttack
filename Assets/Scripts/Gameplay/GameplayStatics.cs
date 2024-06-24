using System;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

[Serializable]
public struct ExplosionVFXConfig
{
    public GameObject Prefab;
    public float Tnt;
}

public class GameplayStatics : Singleton<GameplayStatics>
{
    [SerializeField] private GameObject _antiAirExplosionPrefab;

    [SerializeField] private float _visualTntMultiplier = 100.0f;
    [SerializeField] private float _tntToMetersMultiplies = 0.5f;
    [SerializeField] private AnimationCurve _explosionDamageCurve;
    [SerializeField] private float ObstacleDamageReduction = 0.5f;
    [SerializeField] private GameObject _vfxPrefab;

    [SerializeField] private ExplosionVFXConfig[] _explosions;

    private Pool<VFXRecycler> _airVfxPool;

    private Pool<VFXRecycler>[] _explosionsVfx;

    private Pool<VFXRecycler> _vfx;

    private void Start()
    {
        Instance._airVfxPool = new Pool<VFXRecycler>(
            GetInstance,
            (vfx) => vfx.Recycle(),
            (vfx) => vfx.Deactivate(), 5);

        _explosionsVfx = new Pool<VFXRecycler>[_explosions.Length];

        for (int i = 0; i < _explosions.Length; i++)
        {
            int xui = i;

            _explosionsVfx[i] = new Pool<VFXRecycler>(
                () => Instantiate(_explosions[xui].Prefab).GetComponent<VFXRecycler>(),
                (vfx) => vfx.Recycle(),
                (vfx) => vfx.Deactivate(), 10);

        }

        _vfx = new Pool<VFXRecycler>(
            () => Instantiate(_vfxPrefab).GetComponent<VFXRecycler>(),
            (vfx) => vfx.Recycle(),
            (vfx) => vfx.Deactivate(), 10);

    }

    public VFXRecycler GetInstance()
    {

        return Instantiate(_antiAirExplosionPrefab).GetComponent<VFXRecycler>();
    }

    public static void SpawnExplosion(float tnt, Vector3 position)
    {
        var config = FindSuitableExplosion(tnt, out int index);
        var explosion = Instance._explosionsVfx[index].Get();

        explosion.Initialize(Instance._explosionsVfx[index]);

        explosion.transform.position = position;

        explosion.transform.localScale = Vector3.one * (1.0f + ((tnt - config.Tnt) / Instance._visualTntMultiplier));

        float radius = Instance._tntToMetersMultiplies * tnt;
        var colliders = Physics.OverlapSphere(position, radius);

        foreach (var collider in colliders)
        {
            if (collider.GetHealth(out var health))
            {
                float damageMultiplier = 1.0f;
                float distance = 0.0f;

                if (Physics.Raycast(position, (collider.transform.position - position).normalized, out var hitInfo, radius))
                {
                    if (hitInfo.collider.gameObject == collider.gameObject)
                        distance = hitInfo.distance;
                    else
                        damageMultiplier *= Instance.ObstacleDamageReduction;
                }

                if (distance == 0.0f)
                    distance = Mathf.Clamp01(Vector3.Distance(position, collider.transform.position));

                float damage = tnt * Instance._explosionDamageCurve.Evaluate(distance / radius);

                health.TakeDamage(null, damage * damageMultiplier);
            }
        }
    }

    public static Transform SpawnVfx(VisualEffectAsset effect, Vector3 position, float lifeTime)
    {
        VFXRecycler vfx = Instance._vfx.Get();
        vfx.transform.position = position;

        vfx.Initialize(Instance._vfx, effect, lifeTime);
        return vfx.transform;
    }

    public static Transform SpawnVfx(VisualEffectAsset effect, Vector3 position, Vector3 forward, float lifeTime)
    {
        VFXRecycler vfx = Instance._vfx.Get();
        vfx.transform.position = position;
        vfx.transform.forward = forward;

        vfx.Initialize(Instance._vfx, effect, lifeTime);
        return vfx.transform;
    }

    private static ExplosionVFXConfig FindSuitableExplosion(float tnt, out int index)
    {
        ExplosionVFXConfig config = Instance._explosions.First();

        index = 0;

        for (int i = 1; i < Instance._explosions.Length; i++)
        {
            var newConfig = Instance._explosions[i];

            if (newConfig.Tnt <= tnt && newConfig.Tnt > config.Tnt)
            {
                config = newConfig;
                index = i;
            }
        }

        return config;
    }


    public static void SpawnAntiAirExplosion(Vector3 position, float damage, float radius)
    {
        var explosion = Instance._airVfxPool.Get();

        explosion.transform.position = position;
        explosion.transform.rotation = Quaternion.identity;

        explosion.Initialize(Instance._airVfxPool);

        var colliders = Physics.OverlapSphere(position, radius);

        foreach (var collider in colliders)
        {
            if (collider.GetHealth(out var health))
            {
                float distance = Mathf.Clamp01(Vector3.Distance(position, collider.transform.position));

                damage *= Instance._explosionDamageCurve.Evaluate(distance / radius);

                health.TakeDamage(null, damage);
            }
        }
    }
}
