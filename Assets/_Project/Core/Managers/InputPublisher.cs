using UnityEngine.InputSystem;

public class InputPublisher : Singleton<InputPublisher>, ControlActions.IGameplayActions
{
    private EventManager _eventManager;
    private ControlActions _controlActions;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
        _controlActions = new();
        _controlActions.Gameplay.SetCallbacks(this);
    }

    private void OnEnable()
    {
        _controlActions.Enable();
    }

    private void OnDisable()
    {
        _controlActions.Disable();
    }

    public void OnMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _eventManager.SendPauseGame();
        }
    }
}
