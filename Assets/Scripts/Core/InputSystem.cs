using UnityEngine;
using UnityEngine.Events;

public class InputSystem : GameSystem
{
    public event UnityAction LaunchBomb;
    public event UnityAction GunAttack;

    public Vector3 RotationDirection;

    public void InvokeLaunchBombEvent()
    {
        LaunchBomb?.Invoke();
    }

    public void InvokeGunAttackEvent()
    {
        GunAttack?.Invoke();
    }
}
