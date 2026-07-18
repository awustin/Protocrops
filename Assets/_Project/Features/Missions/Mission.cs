using System;
using System.Collections.Generic;
using UnityEngine;

public enum TaskStatus
{
    Unkwown,
    Pending,
    InProgress,
    Complete,
}

[CreateAssetMenu(fileName = "Mission", menuName = "Scriptable Objects/Mission")]
public class Mission : ScriptableObject, IDefaultable<Mission>
{
    private class MissionTaskTracker
    {
        private readonly List<(Task Task, TaskStatus Status)> _tasks = new();
        public int Count => _tasks.Count;
        public bool IsAllComplete => _tasks.TrueForAll(task => task.Status == TaskStatus.Complete);

        public MissionTaskTracker() { }

        public void Add(Task task, TaskStatus status = TaskStatus.Pending) => _tasks.Add((task, status));
        public List<(Task Task, TaskStatus Status)> GetList() => _tasks;

        public Task GetTask(int index)
        {
            if (index < 0 || index >= _tasks.Count)
                return Task.Default;

            return _tasks[index].Task;
        }

        public TaskStatus GetStatus(int index)
        {
            if (index < 0 || index >= _tasks.Count)
                return TaskStatus.Unkwown;

            return _tasks[index].Status;
        }

        public bool UpdateStatus(int index, TaskStatus status)
        {
            if (index < 0 || index >= _tasks.Count)
                return false;

            _tasks[index] = (_tasks[index].Task, status);

            return true;
        }
    }

    public bool IsEnabled { get; private set; } = false;
    public bool IsComplete => _tracker.IsAllComplete;
    [SerializeField] private List<Task> _taskSequence = new();
    private readonly MissionTaskTracker _tracker = new();
    private int _current = 0;

    public static Mission Default => CreateInstance<Mission>();

    // https://docs.unity3d.com/ScriptReference/ScriptableObject.Awake.html OnEnable initialises on entering play mode
    private void OnEnable()
    {
        foreach (var task in _taskSequence)
        {
            _tracker.Add(task);
        }

        _current = 0;
    }

    public void SetEnabled(bool value) => IsEnabled = value;
    public Task GetCurrentTask() => IsEnabled ? _tracker.GetTask(_current) : Task.Default;

    public void StartMission()
    {
        _current = 0;
        IsEnabled = true;
    }

    public void CompleteCurrentTask()
    {
        if (!IsEnabled)
            return;

        _tracker.UpdateStatus(_current, TaskStatus.Complete);

        if (_current >= _taskSequence.Count - 1)
            return;

        _current++;
        _tracker.UpdateStatus(_current, TaskStatus.InProgress);
    }
}
