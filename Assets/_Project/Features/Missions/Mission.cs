using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public MissionData Data
    {
        get { return _data; }
        set { _data = value; }
    }
    public bool IsComplete = false; //Ask tracker
    public bool IsEnabled => false;
    public TaskData CurrentTask => _data.TasksData[_currentTask];
    public TaskData NextTask => (_currentTask + 1 < _data.TasksData.Count)
        ? _data.TasksData[_currentTask + 1]
        : TaskData.Default;
    public List<TaskData> TasksData => _data.TasksData;

    [SerializeField] private MissionData _data;
    private int _currentTask = 0;
    private bool _enabled = false;

    public void StartMission(MissionData missionData)
    {
        _data = missionData;
        _currentTask = 0;
        _enabled = true;
    }
}