using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Missile")]
public class MissileConfig : ScriptableObject
{
    [SerializeField] private float _tnt;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private GameObject _view;

    public float Tnt => _tnt;
    public float LifeTime => _lifeTime;
    public float Speed => _speed;
    public float RotationSpeed => _rotationSpeed;
    public GameObject View => _view;
}
