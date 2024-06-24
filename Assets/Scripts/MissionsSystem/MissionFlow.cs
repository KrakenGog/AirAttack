using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionFlow : MonoBehaviour
{
    public event UnityAction OnPlayerLost;
    public event UnityAction OnPlayerWon;


    [SerializeField] private List<GameObserver> _missionAims;
    [SerializeField] private List<GameObserver> _loseConditions;
    [SerializeField] private Transform _playerStart;

    //[SerializeField] private List<GroundAIController> _playerTeam;
    //[SerializeField] private List<GroundAIController> _aiTeam;

    public List<GameObserver> MissionAims => _missionAims;
    public List<GameObserver> LostConditions => _loseConditions;
    public Transform PlayerStart => _playerStart;


    public void Initialize(GameSystemsRegistry gameSystemRegistry)
    {

        EntitiesRegistry entitiesRegistry = gameSystemRegistry.GetSystem<EntitiesRegistry>();

        foreach (var controller in FindObjectsOfType<GroundAIController>())
        {
            controller.Initialize(gameSystemRegistry);
            entitiesRegistry.AddInSituableTeam(controller);
        }

        foreach (var controller in FindObjectsOfType<PlaneAIController>())
        {
            controller.Initialize(gameSystemRegistry);
        }


        foreach (GameObserver missionAim in _missionAims)
        {
            missionAim.Initialize(gameSystemRegistry);
            missionAim.OnReached += OnMissionAimComplete;
        }

        foreach (GameObserver condition in _loseConditions)
        {
            condition.Initialize(gameSystemRegistry);
            condition.OnReached += OnLoseConditionReached;
        }


    }


    private void OnLoseConditionReached(GameObserver loseCondition)
    {
        OnPlayerLost?.Invoke();
    }

    private void OnMissionAimComplete(GameObserver missionAim)
    {
        
    }
}
