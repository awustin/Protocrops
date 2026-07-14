using System;

public enum EventName
{
    Unknown,
}

public class EventManager : Singleton<EventManager>
{
    public event Action
        StartGame,
        PauseGame;

    public void SendStartGame()
    {
        StartGame?.Invoke();
    }

    public void SendPauseGame()
    {
        PauseGame?.Invoke();
    }

    public void SendGameEvent(EventName name = EventName.Unknown)
    {
        if (name == EventName.Unknown) return;
    }
}
