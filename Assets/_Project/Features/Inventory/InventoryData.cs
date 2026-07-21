using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "Scriptable Objects/Inventory/InventoryData")]
public class InventoryData : ScriptableObject
{
    private readonly InventoryItemsTracker _tracker = new();
    public List<string> ToStringList => _tracker.ToStringList();

    public bool AddItem(GameItem item, int quantity) => _tracker.TryAddItem(item, quantity);
}

public class InventoryItem : IDefaultable<InventoryItem>
{
    public int Quantity = 1;
    public static InventoryItem Default => new();

    public InventoryItem() { }
    public InventoryItem(int quantity)
    {
        Quantity = quantity;
    }
}

public class InventoryItemsTracker
{
    private readonly Dictionary<GameItem, InventoryItem> _items = new(new ByItemNameComparer());
    public int Count => _items.Count;

    public InventoryItemsTracker() { }

    public bool TryAddItem(GameItem item, int quantity)
    {
        if (quantity <= 0)
            return false;

        if (_items.TryGetValue(item, out InventoryItem inventoryItem))
        {
            int tempResult = inventoryItem.Quantity + quantity;

            if (tempResult < 0)
                return false;

            if (tempResult == 0)
                _items.Remove(item);
            else
                inventoryItem.Quantity = tempResult;

            return true;
        }

        _items.Add(item, new(quantity));

        return true;
    }

    public bool RemoveItem(GameItem item) => _items.Remove(item);

    public List<string> ToStringList()
    {
        List<string> list = new();

        foreach (var gameItem in _items.Keys)
        {
            list.Add($"{gameItem.Name}: {_items[gameItem].Quantity}");
        }

        return list;
    }
}

public class ByItemNameComparer : IEqualityComparer<GameItem>
{
    public bool Equals(GameItem x, GameItem y) => x.Name == y.Name;
    public int GetHashCode(GameItem obj) => obj.Name.GetHashCode();
}