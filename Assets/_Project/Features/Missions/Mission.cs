using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public MissionData Data
    {
        get { return _data; }
        set { _data = value; }
    }
    public bool IsComplete => _tasksStatus.TrueForAll(match => match.status == TaskStatus.Complete);
    public bool IsEnabled => _enabled;
    public TaskData CurrentTask => _data.TasksData[_current];
    public TaskData NextTask => (_current + 1 < _data.TasksData.Count)
        ? _data.TasksData[_current + 1]
        : TaskData.Default;
    public List<TaskData> TasksData => _data.TasksData;

    [SerializeField] private MissionData _data;
    private int _current = 0;
    private readonly List<(TaskData task, TaskStatus status)> _tasksStatus = new();
    private bool _enabled = false;
    [SerializeField] List<string> _debugTaskStatus = new();

    public void SetEnabled(bool value) => _enabled = value;

    private void LateUpdate()
    {
        _debugTaskStatus.Clear();
        foreach (var (task, status) in _tasksStatus)
        {
            _debugTaskStatus.Add($"{task.Name}: {status}");
        }
    }

    public void StartMission(MissionData missionData)
    {
        _data = missionData;
        _current = 0;
        _enabled = true;

        foreach (TaskData task in missionData.TasksData)
        {
            _tasksStatus.Add((task, TaskStatus.Pending));
        }

        UpdateCurrentStatus(TaskStatus.InProgress);
    }

    public bool CompleteCurrentTask()
    {
        UpdateCurrentStatus(TaskStatus.Complete);

        return IsComplete;
    }

    public bool TryStartNexTask(out TaskData task)
    {
        int next = _current + 1;

        if (next >= 0 && next < _data.TasksData.Count)
        {
            _current = next;
            task = _data.TasksData[_current];
            UpdateCurrentStatus(TaskStatus.InProgress);

            return true;
        }

        task = TaskData.Default;
        return false;
    }

    private void UpdateCurrentStatus(TaskStatus status)
    {
        if (_data.TasksData[_current])
        {
            _tasksStatus[_current] = (
                _tasksStatus[_current].task,
                status
            );
        }
    }
}