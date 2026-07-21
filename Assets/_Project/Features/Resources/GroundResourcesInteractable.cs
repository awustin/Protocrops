using UnityEngine;

public class GroundResourcesInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GroundResourcesManager _manager;

    public void Interact(Transform interactor)
    {
        _manager.CollectResources();
    }
}