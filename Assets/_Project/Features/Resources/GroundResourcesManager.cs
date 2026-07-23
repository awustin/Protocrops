using UnityEngine;

public class GroundResourcesManager : MonoBehaviour
{
    [SerializeField] private GameItemData _itemToCollect;
    private EventManager _eventManager;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
    }

    public void CollectResources()
    {
        int quantity = Random.Range(1, 4);
        _eventManager.SendGameEvent(EventName.CollectItem, (_itemToCollect, quantity));
    }
}