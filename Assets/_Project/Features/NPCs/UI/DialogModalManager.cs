using System.Collections;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogModalManager : MonoBehaviour
{
    [CreateProperty] public string Text { get; set; }

    [SerializeField] private PanelRenderer _dialogPanel;
    private EventManager _eventManager;
    private static readonly WaitForSeconds _waitForSeconds5 = new(5f);
    private Coroutine _dialogCoroutine;

    private void Awake()
    {
        _dialogPanel.RegisterUIReloadCallback(BindTextPropery);
        _dialogPanel.gameObject.SetActive(false);
        _eventManager = EventManager.Instance;
    }

    private void OnEnable()
    {
        _eventManager.NPCSpeaks += OnNPCSpeaks;
    }

    private void OnDisable()
    {
        _eventManager.NPCSpeaks -= OnNPCSpeaks;
    }

    private void BindTextPropery(PanelRenderer panel, VisualElement rootElement)
    {
        rootElement.dataSource = this;
    }

    private void OnNPCSpeaks(object sender, string e)
    {
        if (_dialogCoroutine != null)
        {
            StopCoroutine(_dialogCoroutine);
        }

        _dialogPanel.gameObject.SetActive(true);
        Text = e;

        _dialogCoroutine = StartCoroutine(HideDialogAfterDelay());
    }

    private IEnumerator HideDialogAfterDelay()
    {
        yield return _waitForSeconds5;

        Text = string.Empty;
        _dialogPanel.gameObject.SetActive(false);

        _dialogCoroutine = null;
    }
}
