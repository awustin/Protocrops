using System;
using System.Collections.Generic;

public class Inventory
{
    private readonly InventoryItemsTracker _tracker = new();
    public List<GameItem> ItemsList => _tracker.GetList();
    public List<string> ToStringList => _tracker.ToStringList();

    public bool AddItem(GameItemData item, int quantity) => _tracker.TryAddItem(item, quantity);
    public void Clear() => _tracker.ClearItems();
}

public class InventoryItemsTracker
{
    public int Count => _items.Count;
    private readonly List<GameItem> _items = new();

    public List<GameItem> GetList() => _items;
    public void ClearItems() => _items.Clear();
    public List<string> ToStringList() =>
        _items.ConvertAll(new Converter<GameItem, string>(GameItemToString));
    public bool RemoveItem(GameItemData itemData) => _items.Remove(new GameItem(itemData, 0));
    public bool TryAddItem(GameItemData itemData, int quantity)
    {
        if (itemData == null || quantity <= 0)
            return false;

        GameItem attempt = new(itemData, quantity);
        GameItem existing = _items.Find(i => i.Equals(attempt));

        if (existing == null)
        {
            _items.Add(attempt);
            return true;
        }

        if (!existing.TryAddAmount(quantity, out int result))
            return false;

        if (result <= 0)
            _items.Remove(existing);

        return true;
    }

    private string GameItemToString(GameItem item) => item.ToString();
}
