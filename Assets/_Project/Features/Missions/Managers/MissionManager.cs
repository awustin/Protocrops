using UnityEngine;
using System;

public enum TaskStatus
{
    Unkwown,
    Pending,
    InProgress,
    Complete
}

public class MissionManager : Singleton<MissionManager>
{
    public Mission CurrentMission => _mission;
    public TaskData CurrentTask => CurrentMission.CurrentTask;

    private EventManager _eventManager;
    [SerializeField] private TaskEventObserver _taskEventObserver;
    private Mission _mission;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
    }

    private void OnEnable()
    {
        _eventManager = EventManager.Instance;
        _eventManager.NewMission += SetNewMission;
        _eventManager.NextTask += CompleteCurrentAndMoveToNextTask;
    }

    private void OnDisable()
    {
        _eventManager.NewMission -= SetNewMission;
        _eventManager.NextTask -= CompleteCurrentAndMoveToNextTask;

        foreach (Mission mission in GetComponents<Mission>())
        {
            Destroy(mission);
        }
    }

    private void SetNewMission(object sender, object obj)
    {
        try
        {
            MissionData missionData = (MissionData)obj;

            GameObject missionObject = new(missionData.DisplayName);
            missionObject.transform.SetParent(transform, false);
            Mission mission = missionObject.AddComponent<Mission>();

            mission.StartMission(missionData);

            TaskData currentTask = mission.CurrentTask;

            if (currentTask.Type == TaskType.EventEffect)
            {
                _taskEventObserver.SetTask(mission.CurrentTask);
                _taskEventObserver.Activate();
            }

            // To do: add a Task Poll Observer

            _mission = mission;
        }
        catch (Exception e)
        {
            Debug.Log($"Tried to start a mission but something failed: {e}");
        }
    }

    private void CompleteCurrentAndMoveToNextTask()
    {
        if (_mission.IsEnabled && !_mission.IsComplete)
        {
            bool isMissionComplete = _mission.CompleteCurrentTask();

            if (isMissionComplete)
            {
                CompleteMission();
            }
            else if (_mission.TryStartNexTask(out TaskData task))
            {
                _taskEventObserver.SetTask(task);
                _taskEventObserver.Activate();
            }
        }
    }

    private void CompleteMission()
    {
        _taskEventObserver.ClearTask();
        _taskEventObserver.Deactivate();
        _mission.SetEnabled(false);
    }
}
