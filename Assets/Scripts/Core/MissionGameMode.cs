using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionGameMode : MonoBehaviour
{
    [SerializeField] private GameSystemsRegistry _gameSystemsRegistry;
    [SerializeField] private InputSystem _inputSystem;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private CameraMover _cameraMover;
    [SerializeField] private EntitiesRegistry _entitiesRegistry;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private PoolsBase _poolsBase;
    [SerializeField] private MissionConfig _testConfig;

    [SerializeField] private List<GroundVehicle> _groundVehicles;

    private void Start()
    {
        _gameSystemsRegistry.Initialize();

        Application.targetFrameRate = 60;

        _playerController.Initialize(_inputSystem);

        

        _poolsBase.Initialize();

        _entitiesRegistry.Initialize();

        _gameSystemsRegistry.AddSystem(_gameUI);
        _gameSystemsRegistry.AddSystem(_inputSystem);
        _gameSystemsRegistry.AddSystem(_entitiesRegistry);



        _entitiesRegistry.AddPlayer(_playerController);


        MissionFlow missionFlow = Instantiate(_testConfig.MissionFlow);
        missionFlow.transform.position = Vector3.zero;

        missionFlow.Initialize(_gameSystemsRegistry);

        _playerController.Rigidbody.MovePosition(missionFlow.PlayerStart.position);
        _playerController.transform.forward = missionFlow.PlayerStart.forward;

        _gameUI.Initialize(_inputSystem, missionFlow);

        missionFlow.OnPlayerLost += () => StartCoroutine(Restart());
        
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        _gameSystemsRegistry.UpdateAllSystems(Time.deltaTime);

        _cameraMover.MoveToTarget(_playerController.transform, _playerController.Rigidbody.velocity);
    }
}
