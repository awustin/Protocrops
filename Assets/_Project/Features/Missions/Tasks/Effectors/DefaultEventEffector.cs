using UnityEngine;

[CreateAssetMenu(fileName = "DefaultEventEffector", menuName = "Scriptable Objects/Missions/Effectors/DefaultEventEffector")]
public class DefaultEventEffector : EventEffector, IEventEffector
{
    public override EventName EventNameSubscribed => EventName.Unknown;

    public override bool OnEventEffected(EventName eventName) => true;
}