using UnityEngine;

public class ItemResource : MonoBehaviour, IInteractable
{
    private ResourceManager _resourceManager;
    [SerializeField] private int _value;

    void Awake()
    {
        _resourceManager = ResourceManager.Instance;
    }

    public void Interact()
    {
        _resourceManager.Add(_value);
    }
}