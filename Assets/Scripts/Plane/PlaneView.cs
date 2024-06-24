using UnityEngine;
using UnityEngine.VFX;

public enum EngineSmoke
{
    None, WhiteSmoke, DarkSmoke
}

public class PlaneView : MonoBehaviour
{
    [SerializeField] private float _rotationAngle = 20.0f;
    [SerializeField] private float _rotationSpeed = 5.0f;
    [SerializeField] private Transform View;

    [SerializeField] private Transform[] _rearFlaps;
    [SerializeField] private Transform[] _ailerons;
    [SerializeField] private Transform _tail;
    [SerializeField] private float _rearFlapsAngle = 15.0f;
    [SerializeField] private float _aileronsAngle = 20.0f;
    [SerializeField] private float _tailAngle = 30.0f;

    [SerializeField] private Transform _propeller;
    [SerializeField] private GameObject _whiteSmokeTrailPrefab;
    [SerializeField] private GameObject _darkSmokeTrailPrefab;

    [SerializeField] private Transform[] _destructableParts;
    [SerializeField] private float _partChanceToDetach = 0.3f;

    private PlaneMover _mover;
    private Health _health;

    private Quaternion[] _flapsDefaultRotation;
    private Quaternion[] _aileronsDefaultRotation;
    private Vector3 _tailDefaultRotation;

    private EngineSmoke _engineSmokeState = EngineSmoke.None;
    private GameObject _engineSmoke;

    public void Initialize(PlaneMover mover, Health health)
    {
        _mover = mover;
        _health = health;

        _flapsDefaultRotation = new Quaternion[_rearFlaps.Length];
        _aileronsDefaultRotation = new Quaternion[_ailerons.Length];

        for (int i = 0; i < _rearFlaps.Length; i++)
            _flapsDefaultRotation[i] = _rearFlaps[i].localRotation;

        for (int i = 0; i < _ailerons.Length; i++)
            _aileronsDefaultRotation[i] = _ailerons[i].localRotation;

        _tailDefaultRotation = _tail.localRotation.eulerAngles;

        _health.OnHit += OnHit;
        _health.OnDeath += OnDeath;
    }

    private void OnDeath(Component instigator)
    {
        enabled = false;

        foreach (var part in _destructableParts)
        {
            if (UnityEngine.Random.value <= _partChanceToDetach)
            {
                part.parent = null;

                part.gameObject.AddComponent<Rigidbody>().velocity = _mover.Rigidbody.velocity;
                part.gameObject.layer = 7;

                var particles = Instantiate(_darkSmokeTrailPrefab, part.position, Quaternion.identity);
                particles.transform.SetParent(part.transform);
            }
        }
    }

    private void OnHit(Component instigator, float damage)
    {
        if (_health.PercentFilled < 0.2 && _engineSmokeState != EngineSmoke.DarkSmoke)
        {
            if (_engineSmokeState == EngineSmoke.WhiteSmoke)
                _engineSmoke.GetComponentInChildren<VisualEffect>().Stop();

            _engineSmoke = Instantiate(_darkSmokeTrailPrefab, _propeller.position, Quaternion.identity);
            _engineSmoke.transform.SetParent(_propeller.transform);
            _engineSmokeState = EngineSmoke.DarkSmoke;
        }
        else if (_health.PercentFilled < 0.4 && _engineSmokeState != EngineSmoke.WhiteSmoke && _engineSmokeState != EngineSmoke.DarkSmoke)
        {
            _engineSmoke = Instantiate(_whiteSmokeTrailPrefab, _propeller.position, Quaternion.identity);
            _engineSmoke.transform.SetParent(_propeller.transform);
            _engineSmokeState = EngineSmoke.WhiteSmoke;
        }
    }

    private void Update()
    {
        float targetAngle = _rotationAngle * -_mover.RotationInput;
        float progress = _rotationSpeed * Time.deltaTime;

        View.localRotation = Quaternion.Lerp(
            View.localRotation,
            Quaternion.Euler(0.0f, 0.0f, targetAngle),
            progress);

        if (_mover.RotationInput == 0.0f)
        {
            for (int i = 0; i < _rearFlaps.Length; i++)
                _rearFlaps[i].localRotation = Quaternion.Lerp(_rearFlaps[i].localRotation, _flapsDefaultRotation[i], progress);

            for (int i = 0; i < _ailerons.Length; i++)
                _ailerons[i].localRotation = Quaternion.Lerp(_ailerons[i].localRotation, _aileronsDefaultRotation[i], progress);

            _tail.localRotation = Quaternion.Lerp(_tail.localRotation, Quaternion.Euler(_tailDefaultRotation), progress);

            return;
        }

        for (int i = 0; i < _rearFlaps.Length; i++)
        {
            var defaultRotation = _flapsDefaultRotation[i].eulerAngles;
            _rearFlaps[i].localRotation = Quaternion.Lerp(_rearFlaps[i].localRotation,
                Quaternion.Euler(-_rearFlapsAngle, defaultRotation.y, defaultRotation.z), progress);
        }

        int majorAileron = _mover.RotationInput > 0.0f ? 1 : 0;

        RotateAileron(majorAileron, _aileronsAngle, progress);
        RotateAileron(1 - majorAileron, -_aileronsAngle, progress);

        _tail.localRotation = Quaternion.Lerp(_tail.localRotation,
            Quaternion.Euler(_tailDefaultRotation.x, _tailDefaultRotation.y, _tailDefaultRotation.z + _tailAngle * -

            Mathf.Sign(_mover.RotationInput)),
            progress);
    }

    private void RotateAileron(int index, float angle, float progress)
    {
        var aileron = _ailerons[index];
        var defaultRotation = _aileronsDefaultRotation[index].eulerAngles;

        aileron.localRotation = Quaternion.Lerp(aileron.localRotation, Quaternion.Euler(angle, defaultRotation.y, defaultRotation.z), progress);
    }
}
