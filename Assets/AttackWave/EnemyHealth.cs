using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    private EventManager _eventManager;
    [SerializeField] private Transform _healthIndicator;
    [SerializeField] private int _totalHealth;
    private int _health;

    void Awake()
    {
        _eventManager = EventManager.Instance;
        _health = _totalHealth;
    }

    void OnEnable()
    {
        _eventManager.OnAttack += OnAttack;
    }

    void OnDisable()
    {
        _eventManager.OnAttack -= OnAttack;
    }

    private void OnAttack(object sender, int damage)
    {

        _health -= damage;
        float progress = (float)_health / _totalHealth * 100;

        TextMeshProUGUI indicator = _healthIndicator.GetComponent<TextMeshProUGUI>();

        indicator.text = $"{progress:F1} %";

        if (_health <= 0)
        {
            gameObject.SetActive(false);
            _eventManager.SendBattleStatus("END");
        }
    }
}