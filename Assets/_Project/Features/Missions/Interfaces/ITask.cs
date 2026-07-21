public enum TaskStatus
{
    Unkwown,
    Pending,
    InProgress,
    Complete,
}

public interface ITask
{
    public TaskData Data { get; }
    public TaskStatus Status { get; }
    public bool IsActive { get; }

    public void Activate();
}