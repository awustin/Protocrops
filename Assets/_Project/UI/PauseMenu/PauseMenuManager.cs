#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private PanelRenderer _panel;
    [SerializeField] private Transform _menu;
    private EventManager _eventManager;
    private GameModeManager _gameModeManager;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
        _gameModeManager = GameModeManager.Instance;
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
        _gameModeManager.SetMode(GameMode.Normal);
        // Resume scenes
    }

    private void OnClickQuit()
    {
        _menu.gameObject.SetActive(false);
        StopPlayMode();
    }

    private void ShowMenu()
    {
        _menu.gameObject.SetActive(true);
    }

    private void StopPlayMode()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #endif
    }
}