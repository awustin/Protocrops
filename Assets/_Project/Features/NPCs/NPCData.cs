using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/NPC/Data")]
public class NPCData : ScriptableObject
{
    public string DisplayName => _displayName;

    [SerializeField] private string _displayName;
}
