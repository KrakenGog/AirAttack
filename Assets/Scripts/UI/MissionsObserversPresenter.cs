using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class MissionsObserversPresenter : MonoBehaviour
{
    [SerializeField] private MissionObserverView _missionAimViewPrefab;
    [SerializeField] private MissionObserverView _loseConditionViewPrefab;
    [SerializeField] private Transform Holder;

    private List<GameObserver> _missionsAims;
    private List<GameObserver> _loseConditions;

    public void Initialize(List<GameObserver> aims, List<GameObserver> loseConditions)
    {
        _missionsAims = aims;
        _loseConditions = loseConditions;
        Render();
    }

    public void Render()
    {
        foreach (var aim in _missionsAims)
        {
            if (!aim.Render)
                continue;

            MissionObserverView missionAimView = Instantiate(_missionAimViewPrefab);
            missionAimView.Initialize(aim);

            missionAimView.transform.SetParent(Holder, false);
        }

        foreach (var loos in _loseConditions)
        {
            if (!loos.Render)
                continue;

            MissionObserverView missionAimView = Instantiate(_loseConditionViewPrefab);
            missionAimView.Initialize(loos);

            missionAimView.transform.SetParent(Holder, false);
        }
    }
}
