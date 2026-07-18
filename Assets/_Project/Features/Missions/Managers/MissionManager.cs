using UnityEngine;
using System.Collections.Generic;

public class MissionManager : Singleton<MissionManager>
{
    public Queue<Mission> MissionQueue { get; private set; } = new();
    public Mission CurrentMission =>
        MissionQueue.Count == 0 ? Mission.Default : MissionQueue.Peek();
    public Task CurrentTask => CurrentMission.GetCurrentTask();

    private EventManager _eventManager;
    [SerializeField] private List<Mission> _debugQueue = new();

    private void Awake()
    {
        _eventManager = EventManager.Instance;
    }

    private void OnEnable()
    {
        _eventManager = EventManager.Instance;
        _eventManager.NewMission += EnqueueMission;
    }

    private void OnDisable()
    {
        _eventManager.NewMission -= EnqueueMission;
    }

    private void LateUpdate()
    {
        _debugQueue.Clear();
        _debugQueue.AddRange(MissionQueue);
    }

    private void EnqueueMission(object sender, object obj)
    {
        try
        {
            Mission mission = (Mission)obj;

            mission.StartMission();
            mission.CompleteCurrentTask();
            MissionQueue.Enqueue(mission);
        }
        catch (System.Exception)
        {
            Debug.Log($"Tried to start mission but sent '{nameof(obj)}' instead");
        }
    }
}
