using UnityEngine;

public class TaskEventObserver : MonoBehaviour, ITask
{
    public TaskData Data => _data;
    public TaskStatus Status => _status;
    public bool IsActive => _isActive;

    private TaskData _data;
    private TaskStatus _status;
    private bool _isActive;
    private EventManager _eventManager;

    private void Awake()
    {
        _data = TaskData.Default;
        _status = TaskStatus.Unkwown;
        _isActive = false;
        _eventManager = EventManager.Instance;
    }

    public void SetTask(TaskData taskData) => _data = taskData;

    public void ClearCurrentTaskAndDeactivate()
    {
        _data = TaskData.Default;
        _status = TaskStatus.Unkwown;
        _isActive = false;
        _eventManager.ClearNotifyTaskObserverSubscribers();
    }

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
            _status = TaskStatus.Complete;
            Deactivate();
        }
    }
}