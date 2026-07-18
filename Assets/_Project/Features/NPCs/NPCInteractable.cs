using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private NPCController _npcController;

    public void Interact(Transform interactor)
    {
        _npcController.OnInteract(interactor);
    }
}