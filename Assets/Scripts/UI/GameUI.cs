using UnityEngine;
using UnityEngine.UI;

public class GameUI : GameSystem
{
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Button _launchBombButton;
    [SerializeField] private ReactiveButton _gunAttackButton;


    private MissionsObserversPresenter _missionsAimPresenter;

    private InputSystem _inputSystem;

    public void Initialize(InputSystem inputSystem, MissionFlow missionFlow)
    {
        _missionsAimPresenter = GetComponent<MissionsObserversPresenter>();

        _inputSystem = inputSystem;

        _launchBombButton.onClick.AddListener(ThrowLaunchBombEvent);

        _missionsAimPresenter.Initialize(missionFlow.MissionAims, missionFlow.LostConditions);
    }

    private void ThrowLaunchBombEvent()
    {
        _inputSystem.InvokeLaunchBombEvent();
    }

    public override void GameUpdate(float delta)
    {
        Vector2 joystickInput = _joystick.Direction;

        Vector3 joystickDirection = new Vector3(joystickInput.x, 0, joystickInput.y);

        _inputSystem.RotationDirection = joystickDirection;

        if (_gunAttackButton.Pressed)
            _inputSystem.InvokeGunAttackEvent();
    }

}
