using UnityEngine;

public abstract class GameSystem : MonoBehaviour
{
    public virtual void GameUpdate(float delta) { }

    public virtual void StopSystem() { }
}
