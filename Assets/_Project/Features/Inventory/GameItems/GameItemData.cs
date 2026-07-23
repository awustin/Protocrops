using UnityEngine;

public enum ItemType
{
    Unknown,
    Resource,
    Module,
    Part
}

[CreateAssetMenu(fileName = "GameItemData", menuName = "Scriptable Objects/Inventory/GameItemData")]
public class GameItemData : ScriptableObject, IDefaultable<GameItemData>
{
    public string Name => _name;
    public string DisplayName => _displayName;
    public ItemType Type => _type;
    public string PrefabPath => _prefabPath;
    public static GameItemData Default => CreateInstance<GameItemData>();

    [SerializeField] private string _name = "";
    [SerializeField] private string _displayName = "";
    [SerializeField] private ItemType _type = ItemType.Unknown;
    [SerializeField] public string _prefabPath = "";
}