using UnityEngine;

public enum GameMode
{
    Normal,
    Building,
    UI,
}

public class GameModeManager : Singleton<GameModeManager>
{
    public GameMode CurrentMode { get; private set; }
    private EventManager _eventManager;
    [SerializeField] private PlayerInteractor _playerInteractor;
    [SerializeField] private PlayerBuilder _playerBuilder;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
        CurrentMode = GameMode.Normal;
        _playerInteractor.enabled = true;
        _playerBuilder.enabled = false;
    }

    public void ToggleBuildingMode()
    {
        SetMode(CurrentMode == GameMode.Building
            ? GameMode.Normal
            : GameMode.Building);

        _playerInteractor.enabled = CurrentMode == GameMode.Normal;
        _playerBuilder.enabled = CurrentMode == GameMode.Building;
    }

    public void SetMode(GameMode mode)
    {
        if (CurrentMode == mode)
            return;

        CurrentMode = mode;
        // _eventManager.SendGameModeChange(mode);

        _playerInteractor.enabled = CurrentMode == GameMode.Normal;
        _playerBuilder.enabled = CurrentMode == GameMode.Building;
    }
}