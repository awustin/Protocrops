using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    public int resources = 0;
    [SerializeField] private Transform _infoCanvas;

    public void Add(int amount)
    {
        resources += amount;
        PrintResourcesMessage(resources);
    }

    public int SubtractAll()
    {
        int current = resources;
        resources = 0;
        PrintResourcesMessage(resources);

        return current;
    }

    public bool TryConsumeResources(int amount)
    {
        if (amount > resources) return false;

        resources -= amount;
        PrintResourcesMessage(resources);

        return true;
    }

    private void PrintResourcesMessage(int amount)
    {
        if (_infoCanvas.TryGetComponent(out IPrintable resourcesText))
        {
            resourcesText.PrintResources(amount);
        }
    }
}