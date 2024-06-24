using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAirAttack : MonoBehaviour
{
    [SerializeField] private List<Gun> _guns;

    public void Initialize()
    {
        foreach (var gun in _guns)
        {
            gun.Initialize();
        }
    }

    public void Attack(float time, Vector3 position)
    {
        foreach (var gun in _guns)
        {
            if (gun.CanShoot())
            {
                gun.Shoot(time);
                StartCoroutine(SpawnExplosion(time, position));
            }
        }
    }

    private IEnumerator SpawnExplosion(float time, Vector3 position)
    {
        yield return new WaitForSeconds(time);

        GameplayStatics.SpawnAntiAirExplosion(position, 1, 10);
    }
}
