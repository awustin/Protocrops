using UnityEngine.InputSystem;

public class InputPublisher :
    Singleton<InputPublisher>, ControlActions.IGameplayActions, ControlActions.IPlayerInterruptActions
{
    private EventManager _eventManager;
    private ControlActions _controlActions;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
        _controlActions = new();
        _controlActions.Gameplay.SetCallbacks(this);
        _controlActions.PlayerInterrupt.SetCallbacks(this);
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

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _eventManager.SendInteractCommand();
        }
    }

    public void OnJump(InputAction.CallbackContext context) { }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _eventManager.SendToggleInventoryCommand();
        }
    }
}
