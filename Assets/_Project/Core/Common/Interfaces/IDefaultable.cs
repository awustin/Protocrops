/// <summary>
/// In the current Unity version, the implementation of static properties is not enforced.
/// In place for semantic representation.
/// </summary>
public interface IDefaultable<T>
{
    public static T Default;
}
