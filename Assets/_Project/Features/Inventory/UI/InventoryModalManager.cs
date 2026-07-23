using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryModalManager : MonoBehaviour
{
    public ListView ListViewElement => _listView;
    public int ListCount => _gameItems.Count;
    public int CurrentIndex => _listView.selectedIndex;

    [SerializeField] private PanelRenderer _inventoryPanel;
    private readonly List<GameItem> _gameItems = new();
    private ListView _listView;

    private void Awake()
    {
        _inventoryPanel.RegisterUIReloadCallback(BindList);
        _inventoryPanel.gameObject.SetActive(false);
    }

    public bool ToggleListView(List<GameItem> items, bool isDirty = true)
    {
        if (_inventoryPanel.gameObject.activeSelf)
        {
            _inventoryPanel.gameObject.SetActive(false);
            return false;
        }

        // Regenerate list
        if (isDirty)
        {
            _gameItems.Clear();
            _gameItems.AddRange(items);
            _listView?.RefreshItems();
        }

        _inventoryPanel.gameObject.SetActive(true);
        return true;
    }

    private void BindList(PanelRenderer panelRenderer, VisualElement rootElement)
    {
        _listView = rootElement.Q<ListView>();
        _listView.itemsSource = _gameItems;
        _listView?.Rebuild();
    }

    public void ScrollDown()
    {
        if (!gameObject.activeSelf || _listView == null)
            return;

        int index = Mathf.Clamp(_listView.selectedIndex + 1, 0, _gameItems.Count - 1);

        _listView.SetSelection(index);
        _listView.ScrollToItem(index);
    }

    public void ScrollUp()
    {
        if (!gameObject.activeSelf || _listView == null)
            return;

        int index = Mathf.Clamp(_listView.selectedIndex - 1, 0, _gameItems.Count - 1);

        _listView.SetSelection(index);
        _listView.ScrollToItem(index);
    }

    public GameItem GetAtCurrentIndex() => _gameItems[CurrentIndex];
}