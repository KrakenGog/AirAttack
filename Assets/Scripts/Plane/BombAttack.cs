using System.Collections.Generic;
using UnityEngine;

public class BombAttack : MonoBehaviour
{
    [SerializeField] private List<BombHolder> _bombHolders;
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private GameObject _sight;

    private int _currentBombHolderIndex;

    private GameObject[] _views;

    private Rigidbody _rigidBody;

    private TrajectoryCalculator _trajectoryCalculator;
    private Vector3 _hitPosition;

    public void Initialize(TrajectoryCalculator calculator)
    {
        _rigidBody = GetComponent<Rigidbody>();

        _views = new GameObject[_bombHolders.Count];

        _trajectoryCalculator = calculator;

        RenderBombs();
    }

    private void RenderBombs()
    {
        int index = 0;

        foreach (var bombHolder in _bombHolders)
        {
            if (!bombHolder.RenderBomb)
            {
                index++;
                continue;
            }

            GameObject instance = Instantiate(bombHolder.BombConfig.View);

            instance.transform.SetParent(bombHolder.transform);

            instance.transform.position = bombHolder.transform.position;

            _views[index] = instance;

            index++;
        }
    }

    public bool CanLaunchBomb() => _currentBombHolderIndex < _bombHolders.Count;



    public void LaunchBomb()
    {
        BombConfig bombConfig = _bombHolders[_currentBombHolderIndex].GetBomb();

        Bomb instance = Instantiate(_bombPrefab);

        //instance.transform.position = _bombHolders[_currentBombHolderIndex].transform.position;
        instance.Initialize(bombConfig, 2, _rigidBody.velocity, _rigidBody.angularVelocity, _bombHolders[_currentBombHolderIndex].transform.position);

        if (_bombHolders[_currentBombHolderIndex].IsEmpty)
        {
            RemoveView(_currentBombHolderIndex);
            _currentBombHolderIndex++;
        }
    }

    private void RemoveView(int index)
    {
        if (_views[index] != null)
            Destroy(_views[index]);
    }

    private void Update()
    {
        if (!CanLaunchBomb())
        {
            _sight.SetActive(false);
            return;
        }

        _trajectoryCalculator.StartPosition = _bombHolders[_currentBombHolderIndex].transform.position;
        _trajectoryCalculator.StartVelocity = _rigidBody.velocity;

        var (position, normal, time) = _trajectoryCalculator.Calculate();
        _hitPosition = position;
        if (position == Vector3.zero)
        {
            _sight.SetActive(false);
            return;
        }


        _sight.SetActive(true);
        _sight.transform.position = position + normal * 0.1f;
        _sight.transform.rotation = Quaternion.LookRotation(Quaternion.Euler(-90, 0, 0) * normal, normal);
    }

    public (Vector3 position, Vector3 normal, float time) CaclulateBombTrajectory()
    {
        if (!CanLaunchBomb())
        {
            _sight.SetActive(false);
            return (Vector3.zero, Vector3.zero,0);
        }

        _trajectoryCalculator.StartPosition = _bombHolders[_currentBombHolderIndex].transform.position;
        _trajectoryCalculator.StartVelocity = _rigidBody.velocity;

        return _trajectoryCalculator.Calculate();
    }
}
