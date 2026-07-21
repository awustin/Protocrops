using UnityEngine;

[CreateAssetMenu(fileName = "CompleteOnEventEffector", menuName = "Scriptable Objects/Missions/Effectors/CompleteOnEventEffector")]
public class CompleteOnEventEffector : EventEffector, IEventEffector
{
    public override EventName EventNameSubscribed => EventNameSubscribedEditor;

    public EventName EventNameSubscribedEditor;

    public override bool OnEventEffected(EventName eventName) => eventName == EventNameSubscribed;
}