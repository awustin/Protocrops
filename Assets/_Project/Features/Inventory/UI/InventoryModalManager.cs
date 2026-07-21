using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryModalManager : MonoBehaviour
{
    [SerializeField] private PanelRenderer _inventoryPanel;
    private readonly List<string> _inventoryItems = new();
    private ListView _listView;

    private void Awake()
    {
        _inventoryPanel.RegisterUIReloadCallback(BindList);
        _inventoryPanel.gameObject.SetActive(false);
    }

    public void ToggleListView(List<string> items, bool isDirty = true)
    {
        if (_inventoryPanel.gameObject.activeSelf)
        {
            _inventoryPanel.gameObject.SetActive(false);
            return;
        }

        // Regenerate list
        if (isDirty)
        {
            _inventoryItems.Clear();
            _inventoryItems.AddRange(items);
            _listView?.RefreshItems();
        }

        _inventoryPanel.gameObject.SetActive(true);
    }

    private void BindList(PanelRenderer panelRenderer, VisualElement rootElement)
    {
        _listView = rootElement.Q<ListView>();
        _listView.itemsSource = _inventoryItems;
        _listView?.Rebuild();
    }
}