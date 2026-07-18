using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "Scriptable Objects/Task")]
public class Task : ScriptableObject, IDefaultable<Task>
{
    public string Name = ""; 
    public string Description = "";
    public int Id = -1;

    public static Task Default => CreateInstance<Task>();
}