using System;

public class GameItem : IDefaultable<GameItem>, IEquatable<GameItem>
{
    public GameItemData Data => _data;
    public int Count => _count;
    public GameItem Default => new(GameItemData.Default);

    private readonly GameItemData _data;
    private int _count = 0;

    public GameItem(GameItemData data) => _data = data;
    public GameItem(GameItemData data, int count)
    {
        _data = data;
        _count = count;
    }

    public bool TryAddAmount(int addAmount, out int resultCount)
    {
        resultCount = _count;
        int temp = _count + addAmount;

        if (temp < 0)
            return false;

        resultCount = _count = temp;

        return true;
    }

    public override string ToString() => $"{_data.Name} [{_count}]";

    public bool Equals(GameItem other) =>
        other != null && _data.Name.Equals(other.Data.Name);

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (obj is not GameItem gameItem) return false;
        else return Equals(gameItem);
    }

    public override int GetHashCode() => _data.Name.GetHashCode();
}