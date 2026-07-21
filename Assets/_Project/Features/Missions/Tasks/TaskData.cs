using UnityEngine;

public enum TaskType
{
    Unkwown,
    EventEffect,
    Poll,
}

[CreateAssetMenu(fileName = "TaskData", menuName = "Scriptable Objects/Missions/TaskData")]
public class TaskData : ScriptableObject, IDefaultable<TaskData>
{
    public string Name = "Default";
    public string Description = "";
    public int Id = -1;
    public TaskType Type = TaskType.Unkwown;
    public EventEffector Effector;

    public static TaskData Default => CreateInstance<TaskData>();
}