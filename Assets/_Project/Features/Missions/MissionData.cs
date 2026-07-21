using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionData", menuName = "Scriptable Objects/Missions/MissionData")]
public class MissionData : ScriptableObject, IDefaultable<MissionData>
{
    public string DisplayName => _displayName;
    public List<TaskData> TasksData => _tasksData;

    [SerializeField] private List<TaskData> _tasksData = new();
    [SerializeField] private string _displayName = "Unknown";

    public static MissionData Default => CreateInstance<MissionData>();
}
