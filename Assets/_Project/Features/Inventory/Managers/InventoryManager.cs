using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private InventoryModalManager _viewManager;
    private EventManager _eventManager;
    private GameModeManager _gameModeManager;
    private bool _dirty = false;
    private Inventory _inventory;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
        _gameModeManager = GameModeManager.Instance;
        _inventory = new();
    }

    private void OnEnable()
    {
        _eventManager.CollectItem += AddGameItemDataFromResource;
        _eventManager.ToggleInventory += OnToggleInventoryView;
        _eventManager.UIUp += OnMoveUp;
        _eventManager.UIDown += OnMoveDown;
        _eventManager.UISubmit += SelectGameItem;
    }

    private void OnDisable()
    {
        _eventManager.CollectItem -= AddGameItemDataFromResource;
        _eventManager.ToggleInventory -= OnToggleInventoryView;
        _eventManager.UIUp -= OnMoveUp;
        _eventManager.UIDown -= OnMoveDown;
        _eventManager.UISubmit -= SelectGameItem;
        _inventory.Clear();
    }

    private void AddGameItemDataFromResource(object sender, object e)
    {
        var (item, quantity) = ((GameItemData, int))e;

        _inventory.AddItem(item, quantity);
        _dirty = true;
    }

    private void OnToggleInventoryView()
    {
        bool active = _viewManager.ToggleListView(_inventory.ItemsList, _dirty);
        _dirty = false;

        _gameModeManager.SetMode(active ? GameMode.UI : GameMode.Normal);
    }

    private void SelectGameItem()
    {
        GameItem gameItem = _viewManager.GetAtCurrentIndex();

        if (gameItem.Data.Type == ItemType.Module)
        {
            OnToggleInventoryView();
            _gameModeManager.SetMode(GameMode.Building);
            _eventManager.SendGameEvent(EventName.InventorySelectModule, gameItem.Data.PrefabPath);
        }
    }

    private void OnMoveDown() => _viewManager.ScrollDown();
    private void OnMoveUp() => _viewManager.ScrollUp();
}