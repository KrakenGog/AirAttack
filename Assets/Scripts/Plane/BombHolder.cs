using UnityEngine;

public class BombHolder : MonoBehaviour
{
    [SerializeField] private BombConfig _bombConfig;

    [SerializeField] private int _bombsCount;

    [SerializeField] private bool _renderBombs;

    public bool IsEmpty => _bombsCount == 0;

    public bool RenderBomb => _renderBombs;

    public BombConfig BombConfig => _bombConfig;

    public BombConfig GetBomb()
    {
        _bombsCount -= 1;

        return _bombConfig;
    }


}
