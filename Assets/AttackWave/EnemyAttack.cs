using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EventManager _eventManager;
    [SerializeField] private int damage;
    private float _deltaTimeAcc;

    void Awake()
    {
        _eventManager = EventManager.Instance;
        _deltaTimeAcc = 0;
    }

    void Update()
    {
        _deltaTimeAcc += Time.deltaTime;

        if (_deltaTimeAcc >= 1.0f)
        {
            _eventManager.SendEnemyAttack(damage);
            _deltaTimeAcc = 0;
        }
    }
}