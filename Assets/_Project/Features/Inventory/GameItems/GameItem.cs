using UnityEngine;

public enum ItemType
{
    Unknown,
    Resource,
    Module,
    Part
}

[CreateAssetMenu(fileName = "GameItem", menuName = "Scriptable Objects/Inventory/GameItem")]
public class GameItem : ScriptableObject, IDefaultable<GameItem>
{
    public string Name => _name;
    public string DisplayName => _displayName;
    public ItemType Type => _type;
    public static GameItem Default => CreateInstance<GameItem>();

    [SerializeField] private string _name = "";
    [SerializeField] private string _displayName = "";
    [SerializeField] private ItemType _type = ItemType.Unknown;
}