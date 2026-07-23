using UnityEngine;
using UnityEngine.InputSystem;

public class InputBroker : Singleton<InputBroker>, ControlActions.IPlayerPollActions
{
    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    private bool _isReady = false;

    private ControlActions _controlActions;
    private GameModeManager _gameModeManager;

    private void Awake()
    {
        _gameModeManager = GameModeManager.Instance;
        _controlActions = new();
        _controlActions.PlayerPoll.SetCallbacks(this);

        ClearValues();
        Invoke(nameof(StartInputReading), 1.0f);
    }

    private void OnEnable()
    {
        _controlActions.Enable();
    }

    private void OnDisable()
    {
        _controlActions.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!_isReady || _gameModeManager.CurrentMode == GameMode.UI)
            return;

        if (context.performed)
        {
            Move = context.ReadValue<Vector2>();
        }

        if (context.canceled)
        {
            Move = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (!_isReady || _gameModeManager.CurrentMode == GameMode.UI)
            return;

        Vector2 screenCoordinates = context.ReadValue<Vector2>();

        if (
            screenCoordinates.x > Screen.width * 2
            || screenCoordinates.x < 0
            || screenCoordinates.y > Screen.height
            || screenCoordinates.y < 0
        )
            return;

        Look = screenCoordinates;
    }

    private void StartInputReading()
    {
        _isReady = true;
    }

    private void ClearValues()
    {
        Move = Vector2.zero;
        Look = new(
            Screen.width * 0.5f,
            Screen.height * 0.5f
        );
    }
}
