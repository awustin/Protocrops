using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterHealth : MonoBehaviour
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
        _eventManager.OnEnemyAttack += OnEnemyAttack;
    }

    void OnDisable()
    {
        _eventManager.OnEnemyAttack -= OnEnemyAttack;
    }

    private void OnEnemyAttack(object sender, int damage)
    {

        _health -= damage;
        float progress = (float)_health / _totalHealth * 100;

        TextMeshProUGUI indicator = _healthIndicator.GetComponent<TextMeshProUGUI>();

        indicator.text = $"{progress:F1} %";

        if (_health <= 0)
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}