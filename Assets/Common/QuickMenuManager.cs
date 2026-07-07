using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Input;

public class QuickMenuManager : Singleton<QuickMenuManager>, ProtocropsActions.IPlayerActions
{
    private ProtocropsActions _actions;
    private ProtocropsActions.PlayerActions _playerActions;
    [SerializeField] private Transform _infoCanvas;
    private int _current = 0;
    [SerializeField] private List<Transform> _options;
    public System.Action<Transform> OnModuleSelected;

    public void OnInteract(InputAction.CallbackContext context) { }

    public void OnMove(InputAction.CallbackContext context) {}

    public void OnQuickMenu(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        _current = (_current + 1) % _options.Count;
        Transform selected = _options[_current];
        PrintQuickMenu(GetOptionName(selected));
        OnModuleSelected?.Invoke(selected);
    }

    void Awake()
    {
        PrintQuickMenu("");
        _actions = new ProtocropsActions();
        _playerActions = _actions.Player;
        _playerActions.AddCallbacks(this);
        _options.Insert(0, null);
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

    public Transform GetSelected()
    {
        return _options[_current];
    }

    private string GetOptionName(Transform option)
    {
        return option == null ? "" : option.name;
    }

    private void PrintQuickMenu(string text)
    {
        if (_infoCanvas.TryGetComponent(out IPrintable infoText))
        {
            infoText.Print(text);
        }
    }

    void ProtocropsActions.IPlayerActions.OnMousePosition(InputAction.CallbackContext context) {}
}