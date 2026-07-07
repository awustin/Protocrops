using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Input;

public class Menu : MonoBehaviour, ProtocropsActions.IUIActions
{
    private ProtocropsActions _actions;
    private ProtocropsActions.UIActions _uiActions;

    void Awake()
    {
        _actions = new ProtocropsActions();
        _uiActions = _actions.UI;
        _uiActions.AddCallbacks(this);
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

    public void OnMenuOptions(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        string option = context.control.displayName;

        if (option == "1")
        {
            SceneManager.LoadScene("MoveAndCollectScene");
        }

        if (option == "2") {
            SceneManager.LoadScene("CollectAndBuildScene");
        }

        if (option == "3")
        {
            SceneManager.LoadScene("AttackWaveScene");
        }
    }

    public void OnClick(InputAction.CallbackContext context) { }
}
