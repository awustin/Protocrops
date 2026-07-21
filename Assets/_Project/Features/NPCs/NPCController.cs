using UnityEngine;

public enum NPCState
{
    Unknown,
    Fresh,
    MissionInProgress,
    Completed
}

public class NPCController : MonoBehaviour
{
    public NPCData Data => _data;
    public NPCState State => _state;

    [SerializeField] private NPCData _data;
    [SerializeField] private NPCState _state = NPCState.Fresh;
    [SerializeField] private MissionData _missionData;
    private MissionManager _missionManager;
    private EventManager _eventManager;

    private void Awake()
    {
        _missionManager = MissionManager.Instance;
        _eventManager = EventManager.Instance;
    }

    public void OnInteract(Transform interactor)
    {
        switch (_state)
        {
            case NPCState.Fresh:
                _eventManager.SendGameEvent(EventName.NPCSpeaks, "Start your mission!");
                _eventManager.SendGameEvent(EventName.NewMission, _missionData);
                _state = NPCState.MissionInProgress;
                break;

            case NPCState.MissionInProgress:
                if (!_missionManager.CurrentMission.IsComplete)
                {
                    string taskDescription = _missionManager.CurrentTask.Description;

                    if (taskDescription != "")
                    {
                        _eventManager.SendGameEvent(EventName.NPCSpeaks, $"Your next task is: {_missionManager.CurrentTask.Description}");
                        return;
                    }

                    _eventManager.SendGameEvent(EventName.NPCSpeaks, "You have a mission, I am none to say what's the next step");
                }

                _state = NPCState.Completed;
                _eventManager.SendGameEvent(EventName.NPCSpeaks, "Excellent! You completed your mission!");
                break;

            case NPCState.Completed:
                _eventManager.SendGameEvent(EventName.NPCSpeaks, "Excellent! You completed your mission!");
                // Normal post-mission interaction
                break;
        }
    }
}
