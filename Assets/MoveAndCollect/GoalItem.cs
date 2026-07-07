using UnityEngine;

public class GoalItem : MonoBehaviour, IInteractable
{
    private ResourceManager _resourceManager;
    [SerializeField]
    private Transform _infoCanvas;
    private readonly int _goal = 100;
    [SerializeField]
    private int _current = 0;

    void Awake()
    {
        _resourceManager = ResourceManager.Instance;
    }

    public void Interact()
    {
        if (_current >= 100)
        {
            PrintMessage("No more!");
            return;
        }

        int amount = _resourceManager.SubtractAll();

        if (amount == 0)
        {
            PrintMessage("The mesh demands resources");
            return;
        }

        _current += amount;

        float progress = (float)_current / _goal * 100;
    
        PrintMessage($"Progress: {progress:F2}%");
    }

    private void PrintMessage(string message)
    {
        if (_infoCanvas.TryGetComponent(out IPrintable infoText))
        {
            infoText.Print(message);
        }
    }
}