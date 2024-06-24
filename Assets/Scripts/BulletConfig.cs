using UnityEngine;


[CreateAssetMenu(menuName = "Configs/BulletConfig")]
public class BulletConfig : ScriptableObject
{
    [SerializeField] private GameObject _view;
    [SerializeField] private float _damage;
    [SerializeField] private float _lifeTime;

    public GameObject View => _view;
    public float Damage => _damage;
    public float LifeTime => _lifeTime;
}
