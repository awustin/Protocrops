using UnityEngine;
using UnityEngine.UIElements;


public class LoadMenuManager : MonoBehaviour
{
    [SerializeField] private PanelRenderer _panel;
    private EventManager _eventManager;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
        _panel.RegisterUIReloadCallback(OnUIReload);
    }

    private void OnDestroy()
    {
        _panel.UnregisterUIReloadCallback(OnUIReload);
    }

    private void OnUIReload(PanelRenderer renderer, VisualElement root)
    {
        Button startButton = root.Q<Button>("startButton");

        startButton.clicked -= OnClickStart;
        startButton.clicked += OnClickStart;
    }

    private void OnClickStart()
    {
        _eventManager.SendStartGame();
    }
}