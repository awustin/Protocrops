using UnityEngine;
using UnityEngine.InputSystem;
using Input;

public class CharacterAttack : MonoBehaviour, ProtocropsActions.IPlayerActions
{
    private ProtocropsActions _actions;
    private ProtocropsActions.PlayerActions _playerActions;
    private EventManager _eventManager;
    private int _damage;

    void Awake()
    {
        _actions = new ProtocropsActions();
        _playerActions = _actions.Player;
        _playerActions.AddCallbacks(this);
        _eventManager = EventManager.Instance;
        _damage = 1;
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

    public void BoostDamage(int boost)
    {
        _damage *= boost;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _eventManager.SendAttack(_damage);
        }
    }

    public void OnMove(InputAction.CallbackContext context) { }
    public void OnQuickMenu(InputAction.CallbackContext context) { }
    public void OnMousePosition(InputAction.CallbackContext context) { }
}