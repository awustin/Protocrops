using UnityEngine;

public class ModulesManager : Singleton<ModulesManager>
{
    private EventManager _eventManager;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
    }

    private void OnEnable()
    {
        _eventManager.PlaceModule += OnPlaceModule;
    }

    private void OnDisable()
    {
        _eventManager.PlaceModule -= OnPlaceModule;
    }

    private void OnPlaceModule(object sender, object e)
    {
        var (prefab, position, rotation) = ((GameObject, Vector3, Quaternion))e;

        Instantiate(prefab, position, rotation, transform);
    }
}