using UnityEngine.InputSystem;

public class InputGameUI : Singleton<InputGameUI>, ControlActions.IGameUIActions
{
    private EventManager _eventManager;
    private ControlActions _controlActions;
    private GameModeManager _gameModeManager;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
        _gameModeManager = GameModeManager.Instance;
        _controlActions = new();
        _controlActions.GameUI.SetCallbacks(this);
    }

    private void OnEnable()
    {
        _controlActions.Enable();
    }

    private void OnDisable()
    {
        _controlActions.Disable();
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (context.performed && _gameModeManager.CurrentMode == GameMode.UI)
        {
            _eventManager.SendUIEvent(EventName.UISubmit);
        }
    }

    public void OnMoveUp(InputAction.CallbackContext context)
    {
        if (context.performed && _gameModeManager.CurrentMode == GameMode.UI)
        {
            _eventManager.SendUIEvent(EventName.UIUp);
        }
    }

    public void OnMoveDown(InputAction.CallbackContext context)
    {
        if (context.performed && _gameModeManager.CurrentMode == GameMode.UI)
        {
            _eventManager.SendUIEvent(EventName.UIDown);
        }
    }
}
