using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private PanelRenderer _panel;
    [SerializeField] private Transform _menu;
    private EventManager _eventManager;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
        _panel.RegisterUIReloadCallback(OnUIReload);
    }

    private void OnEnable()
    {
        _eventManager.PauseGame += ShowMenu;    
    }

    private void OnDisable()
    {
        _eventManager.PauseGame -= ShowMenu;
    }

    private void OnDestroy()
    {
        _panel.UnregisterUIReloadCallback(OnUIReload);
    }

    private void OnUIReload(PanelRenderer renderer, VisualElement root)
    {
        VisualElement menu = root.Q<GroupBox>("PauseMenu");
        Button resumeButtom = menu.Q<Button>("ResumeButton");
        Button quitButton = menu.Q<Button>("QuitButton");

        resumeButtom.clicked -= OnClickResume;
        resumeButtom.clicked += OnClickResume;

        quitButton.clicked -= OnClickQuit;
        quitButton.clicked += OnClickQuit;
    }

    private void OnClickResume()
    {
        _menu.gameObject.SetActive(false);
        // Resume scenes
    }

    private void OnClickQuit()
    {
        _menu.gameObject.SetActive(false);
        // End game
    }

    private void ShowMenu()
    {
        _menu.gameObject.SetActive(true);
    }
}