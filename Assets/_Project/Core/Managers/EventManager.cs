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
    InventorySelectModule,
    PlaceModule,
    UIUp,
    UIDown,
    UISubmit,
}

public class EventManager : Singleton<EventManager>
{
    public event Action
        StartGame,
        PauseGame,
        Interact,
        ToggleInventory,
        NextTask,
        UIUp,
        UIDown,
        UISubmit;
    public event Action<EventName> NotifyTaskObserver;
    public event Action<GameMode> GameMode;
    public event EventHandler<float> UpdateScore;
    public event EventHandler<object> NewMission, CollectItem, PlaceModule;
    public event EventHandler<string> NPCSpeaks, InventorySelectModule;

    public void SendStartGame() => StartGame?.Invoke();
    public void SendPauseGame() => PauseGame?.Invoke();
    public void SendInteractCommand()
    {
        Interact?.Invoke();
        SendTaskNotification(EventName.InteractCommand);
    }
    public void SendToggleInventoryCommand() => ToggleInventory?.Invoke();
    public void SendNextTask() => NextTask?.Invoke();
    public void SendGameModeChange(GameMode mode) => GameMode?.Invoke(mode);

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
            case EventName.PlaceModule:
                PlaceModule?.Invoke(this, obj);
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
            case EventName.InventorySelectModule:
                InventorySelectModule?.Invoke(this, str);
                break;
            default:
                break;
        }

        SendTaskNotification(name);
    }

    public void SendUIEvent(EventName name)
    {
        switch (name)
        {
            case EventName.UIUp:
                UIUp?.Invoke();
                break;
            case EventName.UIDown:
                UIDown?.Invoke();
                break;
            case EventName.UISubmit:
                UISubmit?.Invoke();
                break;
            default:
                break;
        }
    }

    public void ClearNotifyTaskObserverSubscribers()
    {
        NotifyTaskObserver = null;
    }

    private void SendTaskNotification(EventName eventName) => NotifyTaskObserver?.Invoke(eventName);
}
