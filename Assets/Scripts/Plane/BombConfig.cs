using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Bomb")]
public class BombConfig : ScriptableObject
{
    [SerializeField] private float _mass;
    [SerializeField] private float _tnt;
    [SerializeField] private GameObject _view;

    public float Mass => _mass;
    public float Tnt => _tnt;
    public GameObject View => _view;
}
