using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    private EventManager _eventManager;

    void Awake()
    {
        _eventManager = EventManager.Instance;
    }

    void OnTriggerEnter()
    {
        _eventManager.SendBattleStatus("ON");
    }
    
    void OnTriggerExit()
    {
        _eventManager.SendBattleStatus("OFF");
    }
}