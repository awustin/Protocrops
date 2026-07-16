using System;

public enum EventName
{
    Unknown,
    UpdateScore,
    NewMission,
}

public class EventManager : Singleton<EventManager>
{
    public event Action
        StartGame,
        PauseGame;

    public event EventHandler<float> UpdateScore;
    public event EventHandler<object> NewMission;

    public void SendStartGame()
    {
        StartGame?.Invoke();
    }

    public void SendPauseGame()
    {
        PauseGame?.Invoke();
    }

    public void SendGameEvent(EventName name, float value)
    {
        switch (name)
        {
            case EventName.UpdateScore:
                UpdateScore?.Invoke(this, value);
                break;
            default:
                return;
        }
    }

    public void SendGameEvent(EventName name, object obj)
    {
        switch (name)
        {
            case EventName.NewMission:
                NewMission?.Invoke(this, obj);
                break;
            default:
                return;
        }
    }
}
