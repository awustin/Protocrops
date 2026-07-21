using System;

public enum EventName
{
    Unknown,
    InteractCommand,
    UpdateScore,
    CollectItem,
    NewMission,
    NPCSpeaks,
    ToggleInventory,
}

public class EventManager : Singleton<EventManager>
{
    public event Action
        StartGame,
        PauseGame,
        Interact,
        ToggleInventory;
    public event Action<EventName> NotifyTaskObserver;
    public event EventHandler<float> UpdateScore;
    public event EventHandler<object> NewMission, CollectItem;
    public event EventHandler<string> NPCSpeaks;

    public void SendStartGame() => StartGame?.Invoke();
    public void SendPauseGame() => PauseGame?.Invoke();
    public void SendInteractCommand()
    {
        Interact?.Invoke();
        SendTaskNotification(EventName.InteractCommand);
    }

    public void SendToggleInventoryCommand() => ToggleInventory?.Invoke();

    public void SendGameEvent(EventName name, float value)
    {
        switch (name)
        {
            case EventName.UpdateScore:
                UpdateScore?.Invoke(this, value);
                break;
            default:
                break;
        }

        SendTaskNotification(name);
    }

    public void SendGameEvent(EventName name, object obj)
    {
        switch (name)
        {
            case EventName.NewMission:
                NewMission?.Invoke(this, obj);
                break;
            case EventName.CollectItem:
                CollectItem?.Invoke(this, obj);
                break;
            default:
                break;
        }

        SendTaskNotification(name);
    }

    public void SendGameEvent(EventName name, string str)
    {
        switch (name)
        {
            case EventName.NPCSpeaks:
                NPCSpeaks?.Invoke(this, str);
                break;
            default:
                break;
        }

        SendTaskNotification(name);
    }

    public void ClearNotifyTaskObserverSubscribers()
    {
        NotifyTaskObserver = null;
    }

    private void SendTaskNotification(EventName eventName)
    {
        NotifyTaskObserver?.Invoke(eventName);
    }
}
