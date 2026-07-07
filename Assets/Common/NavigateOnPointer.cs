using UnityEngine;
using Input;
using UnityEngine.InputSystem;

class NavigateOnPointer : MonoBehaviour, ProtocropsActions.IPlayerActions
{
    private ProtocropsActions _actions;
    private ProtocropsActions.PlayerActions _playerActions;
    private Vector2 _screenOrigin;
    private float _xRotationRate, _yRotationRate;

    void Awake()
    {
        _actions = new ProtocropsActions();
        _playerActions = _actions.Player;
        _playerActions.AddCallbacks(this);

        // Origin of movement is the center of screen
        float xOrigin = Screen.width * 0.5f;
        float yOrigin = Screen.height * 0.5f;
        _screenOrigin = new(xOrigin, yOrigin);

        // For a controlled rotation
        _yRotationRate = (float)(360 * .9f) / Screen.width;
        _xRotationRate = (float)(90 * .9f) / Screen.height;
    }

    void OnDestroy()
    {
        _actions.Dispose();
    }

    void OnEnable()
    {
        _actions.Enable();
    }

    void OnDisable()
    {
        _actions.Disable();
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        // Screen coordinates (1280x720)
        Vector2 mousePos = context.ReadValue<Vector2>();

        PlanarToAngularDistance(mousePos);
    }

    public void OnMove(InputAction.CallbackContext context) { }

    public void OnQuickMenu(InputAction.CallbackContext context) { }
    public void OnInteract(InputAction.CallbackContext context) { }

    private void PlanarToAngularDistance(Vector2 mousePosition)
    {
        // Distance to screen origin (screenPosition) becomes angular translation (rotation)
        Vector2 screenPosition = Translate(mousePosition);

        transform.localEulerAngles = new(
            _xRotationRate * -screenPosition.y,
            _yRotationRate * screenPosition.x,
            0f
        );
    }

    private Vector2 Translate(Vector2 x) => x - _screenOrigin;
}