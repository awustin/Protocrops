using UnityEngine;
using UnityEngine.InputSystem;
using Input;

public class Character : MonoBehaviour, ProtocropsActions.IPlayerActions
{
    private ProtocropsActions _actions;
    private ProtocropsActions.PlayerActions _playerActions;
    private bool _isInteracting;

    void Awake()
    {
        _actions = new ProtocropsActions();
        _playerActions = _actions.Player;
        _playerActions.AddCallbacks(this);
        _isInteracting = false;
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

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && !_isInteracting)
        {
            _isInteracting = true;
            ForwardRaycast();
        }
    }

    public void OnMove(InputAction.CallbackContext context) { }

    public void OnQuickMenu(InputAction.CallbackContext context) { }

    void Update()
    {
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;
        float rayDistance = 2.0f;
        Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow, 0.1f);
    }

    private void ForwardRaycast()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2.0f))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }

        _isInteracting = false;
    }

    void ProtocropsActions.IPlayerActions.OnMousePosition(InputAction.CallbackContext context) {}
}