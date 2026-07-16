using UnityEngine;
using System.Collections.Generic;

public class MissionManager : Singleton<MissionManager>
{
    public Queue<Mission> MissionQueue { get; private set; }
    public Mission CurrentMission =>
        MissionQueue.Count == 0 ? Mission.GetDefault() : MissionQueue.Peek();
    private EventManager _eventManager;
    [SerializeField] private List<Mission> _savedQueue = new();

    private void Awake()
    {
        _eventManager = EventManager.Instance;
    }

    private void OnEnable()
    {
        _eventManager.NewMission += EnqueueMission;
        Load();
    }

    private void OnDisable()
    {
        _eventManager.NewMission -= EnqueueMission;
        Save();
    }

    private void EnqueueMission(object sender, object mission)
    {
        MissionQueue.Enqueue((Mission)mission);
    }

    private void Save()
    {
        _savedQueue.Clear();
        _savedQueue.AddRange(MissionQueue);
    }

    private void Load()
    {
        MissionQueue = new(_savedQueue);
    }
}
