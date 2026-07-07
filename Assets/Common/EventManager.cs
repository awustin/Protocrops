using System;

public class EventManager : Singleton<EventManager>
{
    public event EventHandler<string> OnBattleStatus;
    public event EventHandler<int> OnAttack;
    public event EventHandler<int> OnEnemyAttack;

    public void SendBattleStatus(string status)
    {
        OnBattleStatus?.Invoke(this, status);
    }

    public void SendAttack(int damage)
    {
        OnAttack?.Invoke(this, damage);
    }

    public void SendEnemyAttack(int damage)
    {
        OnEnemyAttack?.Invoke(this, damage);
    }
}