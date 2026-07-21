using UnityEngine;

public class TaskEventObserver : MonoBehaviour
{
    public TaskData Data => _data;
    public bool IsActive => _isActive;

    private TaskData _data;
    [SerializeField] private bool _isActive;
    private EventManager _eventManager;

    private void Awake()
    {
        _data = TaskData.Default;
        _isActive = false;
        _eventManager = EventManager.Instance;
    }

    public void SetTask(TaskData taskData) => _data = taskData;
    public void ClearTask() => _data = TaskData.Default;

    public void Activate()
    {
        _eventManager.NotifyTaskObserver += OnTaskNotification;
        _isActive = true;
    }

    public void Deactivate()
    {
        _eventManager.ClearNotifyTaskObserverSubscribers();
        _isActive = false;
    }

    private void OnDisable()
    {
        _eventManager.ClearNotifyTaskObserverSubscribers();
    }

    private void OnTaskNotification(EventName eventName)
    {
        // Task is successful
        if (_data.Effector != null && _data.Effector.OnEventEffected(eventName))
        {
            Deactivate();
            _eventManager.SendNextTask();
        }
    }
}