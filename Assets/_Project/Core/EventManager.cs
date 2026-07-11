using System;

public enum EventName
{
    Unknown,
}

public class EventManager : Singleton<EventManager>
{
    public event Action StartGame;

    public void SendStartGame()
    {
        StartGame?.Invoke();
    }

    public void SendGameEvent(EventName name = EventName.Unknown)
    {
        if (name == EventName.Unknown) return;
    }
}