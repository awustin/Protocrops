using UnityEngine;

public abstract class EventEffector : ScriptableObject, IEventEffector
{
    public abstract EventName EventNameSubscribed { get; }

    public abstract bool OnEventEffected(EventName eventName);
}