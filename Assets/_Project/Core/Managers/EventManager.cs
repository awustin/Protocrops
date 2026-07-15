using System;

public enum EventName
{
    Unknown,
    UpdateScore,
}

public class EventManager : Singleton<EventManager>
{
    public event Action
        StartGame,
        PauseGame;

    public event EventHandler<float> UpdateScore;

    public void SendStartGame()
    {
        StartGame?.Invoke();
    }

    public void SendPauseGame()
    {
        PauseGame?.Invoke();
    }

    public void SendGameEvent(EventName name, float args)
    {
        switch (name)
        {
            case EventName.UpdateScore:
                UpdateScore?.Invoke(this, args);
                break;
            default:
                return;
        }
    }
}
