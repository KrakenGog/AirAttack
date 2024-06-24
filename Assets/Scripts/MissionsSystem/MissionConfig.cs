using UnityEngine;

[CreateAssetMenu(menuName = "Configs/MissionConfig")]
public class MissionConfig : ScriptableObject
{
    [SerializeField] private MissionFlow _missionFlow;

    public MissionFlow MissionFlow => _missionFlow;
}
