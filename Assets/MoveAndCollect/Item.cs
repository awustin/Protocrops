using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    private ResourceManager _resourceManager;

    void Awake()
    {
        _resourceManager = ResourceManager.Instance;
    }

    public void Interact()
    {
        _resourceManager.Add(1);
    }
}