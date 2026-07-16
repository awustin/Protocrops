using UnityEngine;

public enum MissionStatus
{
    Unkwown,
    Pending,
    InProgress,
    Complete,
}

[CreateAssetMenu(fileName = "MissionStep", menuName = "Scriptable Objects/MissionStep")]
public class MissionStep : ScriptableObject, IDefaultable<MissionStep>
{
    public MissionStatus status = MissionStatus.Pending;
    public string Name;
    public string description;

    public static MissionStep GetDefault()
    {
        MissionStep obj = CreateInstance<MissionStep>();
        obj.status = MissionStatus.Unkwown;
        obj.Name = "";
        obj.description = "";

        return obj;
    }
}