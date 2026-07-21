using UnityEngine;
using System.Collections.Generic;

public class MissionManager : Singleton<MissionManager>
{
    public Queue<Mission> MissionQueue { get; private set; } = new();
    public Mission CurrentMission => MissionQueue.Peek();
    public TaskData CurrentTask => CurrentMission.CurrentTask;

    private EventManager _eventManager;
    [SerializeField] private TaskEventObserver _taskEventObserver;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
    }

    private void OnEnable()
    {
        _eventManager = EventManager.Instance;
        _eventManager.NewMission += EnqueueNewMission;
    }

    private void OnDisable()
    {
        _eventManager.NewMission -= EnqueueNewMission;

        foreach (Mission mission in GetComponents<Mission>())
        {
            Destroy(mission);
        }
    }

    private void EnqueueNewMission(object sender, object obj)
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


            MissionQueue.Enqueue(mission);
        }
        catch (System.Exception e)
        {
            Debug.Log($"Tried to start a mission but something failed: {e.ToString()}");
        }
    }
}
