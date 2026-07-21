public interface IEventEffector
{
    public EventName EventNameSubscribed { get; }

    public bool OnEventEffected(EventName eventName);
}