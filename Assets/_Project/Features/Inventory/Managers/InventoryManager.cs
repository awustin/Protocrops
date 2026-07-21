using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private InventoryData _inventory;
    [SerializeField] private InventoryModalManager _viewManager;
    private EventManager _eventManager;
    private bool _dirty = false;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
    }

    private void OnEnable()
    {
        _eventManager.CollectItem += AddGameItemFromResource;
        _eventManager.ToggleInventory += InventoryPrepareView;
    }

    private void OnDisable()
    {
        _eventManager.CollectItem -= AddGameItemFromResource;
        _eventManager.ToggleInventory -= InventoryPrepareView;
    }

    private void AddGameItemFromResource(object sender, object e)
    {
        var (item, quantity) = ((GameItem, int))e;

        _inventory.AddItem(item, quantity);
        _dirty = true;
    }

    private void InventoryPrepareView()
    {
        _viewManager.ToggleListView(_inventory.ToStringList, _dirty);
        _dirty = false;
    }
}